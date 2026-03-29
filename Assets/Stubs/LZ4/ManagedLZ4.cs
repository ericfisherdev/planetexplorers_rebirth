using System;

/// <summary>
/// Pure managed C# implementation of LZ4 block compression.
/// Replaces the native lz4_dll DllImport calls with cross-platform managed code.
/// Method signatures match the original DllImport declarations exactly.
/// </summary>
public static class ManagedLZ4
{
    // Hash table size for compression (must be power of 2).
    // 4096 entries = 16KB, balancing memory use with match-finding quality.
    private const int HashTableSize = 4096;
    private const int HashTableMask = HashTableSize - 1;

    // Minimum match length per LZ4 spec.
    private const int MinMatch = 4;

    // Number of bytes reserved at end of input that are always emitted as literals.
    // This prevents the compressor from reading past the end of input during match extension.
    private const int LastLiterals = 5;

    // Maximum offset for a back-reference (LZ4 uses 2-byte little-endian offset).
    private const int MaxDistance = 65535;

    /// <summary>
    /// No-op initialization. The native DLL required this; the managed version does not.
    /// Returns 0 to indicate success, matching the original behavior.
    /// </summary>
    public static int LZ4_DllLoad()
    {
        return 0;
    }

    /// <summary>
    /// Compresses source bytes into dest using LZ4 block format.
    /// </summary>
    /// <param name="source">Input byte array</param>
    /// <param name="dest">Output byte array (must be large enough for compressed data)</param>
    /// <param name="isize">Number of bytes to compress from source</param>
    /// <returns>Number of bytes written to dest, or 0 on failure</returns>
    public static int LZ4_compress(byte[] source, byte[] dest, int isize)
    {
        if (isize == 0)
        {
            // Empty input: emit a single token with zero literals and no match.
            if (dest.Length < 1)
                return 0;
            dest[0] = 0;
            return 1;
        }

        int[] hashTable = new int[HashTableSize];
        for (int i = 0; i < HashTableSize; i++)
            hashTable[i] = -1;

        int srcIndex = 0;
        int destIndex = 0;
        int anchor = 0; // Start of the current literal run.
        int srcEnd = isize;
        int srcLimit = srcEnd - LastLiterals; // Stop looking for matches here.

        // Skip the first byte (can't match anything).
        srcIndex = 1;

        while (srcIndex < srcLimit)
        {
            // Find a match using hash of 4 bytes at current position.
            int matchCandidate = FindMatch(source, srcIndex, srcEnd, hashTable);

            if (matchCandidate < 0 || srcIndex - matchCandidate > MaxDistance)
            {
                // No match found, advance.
                UpdateHash(source, srcIndex, srcEnd, hashTable);
                srcIndex++;
                continue;
            }

            // Determine match length.
            int matchLength = CountMatchLength(source, srcIndex, matchCandidate, srcEnd);

            if (matchLength < MinMatch)
            {
                UpdateHash(source, srcIndex, srcEnd, hashTable);
                srcIndex++;
                continue;
            }

            // Emit the literal + match sequence.
            int literalLength = srcIndex - anchor;
            int offset = srcIndex - matchCandidate;

            destIndex = EmitSequence(dest, destIndex, source, anchor, literalLength, offset, matchLength);
            if (destIndex < 0)
                return 0; // Output buffer overflow.

            // Update hash table for positions within the match.
            int matchEnd = srcIndex + matchLength;
            srcIndex += 1;
            while (srcIndex < matchEnd && srcIndex < srcLimit)
            {
                UpdateHash(source, srcIndex, srcEnd, hashTable);
                srcIndex++;
            }
            srcIndex = matchEnd;
            anchor = srcIndex;
        }

        // Emit remaining literals (the last bytes that we never tried to match).
        int lastLiterals = srcEnd - anchor;
        if (lastLiterals > 0)
        {
            destIndex = EmitLiteralsOnly(dest, destIndex, source, anchor, lastLiterals);
            if (destIndex < 0)
                return 0;
        }

        return destIndex;
    }

    /// <summary>
    /// Decompresses source into dest, where the exact uncompressed size (osize) is known.
    /// </summary>
    /// <param name="source">Compressed input</param>
    /// <param name="dest">Output buffer (must be at least osize bytes)</param>
    /// <param name="osize">Expected decompressed size</param>
    /// <returns>Number of compressed bytes consumed, or negative on error</returns>
    public static int LZ4_uncompress(byte[] source, byte[] dest, int osize)
    {
        int srcIndex = 0;
        int destIndex = 0;
        int destEnd = osize;

        while (destIndex < destEnd)
        {
            if (srcIndex >= source.Length)
                return -1;

            // Read token.
            byte token = source[srcIndex++];
            int literalLength = (token >> 4) & 0x0F;
            int matchLength = (token & 0x0F) + MinMatch;

            // Extended literal length.
            if (literalLength == 15)
            {
                int extraByte;
                do
                {
                    if (srcIndex >= source.Length)
                        return -1;
                    extraByte = source[srcIndex++];
                    literalLength += extraByte;
                } while (extraByte == 255);
            }

            // Copy literals.
            if (literalLength > 0)
            {
                if (srcIndex + literalLength > source.Length || destIndex + literalLength > dest.Length)
                    return -1;
                Buffer.BlockCopy(source, srcIndex, dest, destIndex, literalLength);
                srcIndex += literalLength;
                destIndex += literalLength;
            }

            // Check if we've filled the output (last sequence has no match).
            if (destIndex >= destEnd)
                break;

            // Read match offset (2 bytes, little-endian).
            if (srcIndex + 2 > source.Length)
                return -1;
            int offset = source[srcIndex] | (source[srcIndex + 1] << 8);
            srcIndex += 2;

            if (offset == 0)
                return -1; // Zero offset is invalid.

            int matchPos = destIndex - offset;
            if (matchPos < 0)
                return -1; // Invalid back-reference.

            // Extended match length.
            if (matchLength == MinMatch + 15)
            {
                int extraByte;
                do
                {
                    if (srcIndex >= source.Length)
                        return -1;
                    extraByte = source[srcIndex++];
                    matchLength += extraByte;
                } while (extraByte == 255);
            }

            // Copy match (byte-by-byte to handle overlapping references).
            if (destIndex + matchLength > dest.Length)
                return -1;
            for (int i = 0; i < matchLength; i++)
            {
                dest[destIndex + i] = dest[matchPos + i];
            }
            destIndex += matchLength;
        }

        return srcIndex;
    }

    /// <summary>
    /// Decompresses source when the exact output size is not known.
    /// </summary>
    /// <param name="source">Compressed input</param>
    /// <param name="dest">Output buffer</param>
    /// <param name="isize">Number of compressed bytes in source</param>
    /// <param name="maxOutputSize">Maximum allowed output size (dest.Length)</param>
    /// <returns>Number of decompressed bytes written to dest, or negative on error</returns>
    public static int LZ4_uncompress_unknownOutputSize(byte[] source, byte[] dest, int isize, int maxOutputSize)
    {
        int srcIndex = 0;
        int srcEnd = isize;
        int destIndex = 0;
        int destEnd = maxOutputSize;

        while (srcIndex < srcEnd)
        {
            // Read token.
            byte token = source[srcIndex++];
            int literalLength = (token >> 4) & 0x0F;
            int matchLength = (token & 0x0F) + MinMatch;

            // Extended literal length.
            if (literalLength == 15)
            {
                int extraByte;
                do
                {
                    if (srcIndex >= srcEnd)
                        return -1;
                    extraByte = source[srcIndex++];
                    literalLength += extraByte;
                } while (extraByte == 255);
            }

            // Copy literals.
            if (literalLength > 0)
            {
                if (srcIndex + literalLength > srcEnd || destIndex + literalLength > destEnd)
                    return -1;
                Buffer.BlockCopy(source, srcIndex, dest, destIndex, literalLength);
                srcIndex += literalLength;
                destIndex += literalLength;
            }

            // If we've consumed all input, this was the last (literal-only) sequence.
            if (srcIndex >= srcEnd)
                break;

            // Read match offset (2 bytes, little-endian).
            if (srcIndex + 2 > srcEnd)
                return -1;
            int offset = source[srcIndex] | (source[srcIndex + 1] << 8);
            srcIndex += 2;

            if (offset == 0)
                return -1;

            int matchPos = destIndex - offset;
            if (matchPos < 0)
                return -1;

            // Extended match length.
            if (matchLength == MinMatch + 15)
            {
                int extraByte;
                do
                {
                    if (srcIndex >= srcEnd)
                        return -1;
                    extraByte = source[srcIndex++];
                    matchLength += extraByte;
                } while (extraByte == 255);
            }

            // Copy match (byte-by-byte for overlapping references).
            if (destIndex + matchLength > destEnd)
                return -1;
            for (int i = 0; i < matchLength; i++)
            {
                dest[destIndex + i] = dest[matchPos + i];
            }
            destIndex += matchLength;
        }

        return destIndex;
    }

    // --- Private helper methods ---

    /// <summary>
    /// Computes a hash of 4 bytes at the given position for match finding.
    /// </summary>
    private static int Hash4Bytes(byte[] data, int index, int dataLength)
    {
        if (index + 3 >= dataLength)
            return 0;

        uint value = (uint)data[index]
                   | ((uint)data[index + 1] << 8)
                   | ((uint)data[index + 2] << 16)
                   | ((uint)data[index + 3] << 24);

        // Knuth multiplicative hash.
        return (int)((value * 2654435761u) >> 20) & HashTableMask;
    }

    /// <summary>
    /// Looks up and updates the hash table. Returns the previous position at this hash, or -1.
    /// </summary>
    private static int FindMatch(byte[] source, int position, int srcEnd, int[] hashTable)
    {
        int hash = Hash4Bytes(source, position, srcEnd);
        int candidate = hashTable[hash];
        hashTable[hash] = position;
        return candidate;
    }

    /// <summary>
    /// Updates the hash table for the given position without returning the old value.
    /// </summary>
    private static void UpdateHash(byte[] source, int position, int srcEnd, int[] hashTable)
    {
        int hash = Hash4Bytes(source, position, srcEnd);
        hashTable[hash] = position;
    }

    /// <summary>
    /// Counts how many bytes match between source[current..] and source[candidate..].
    /// </summary>
    private static int CountMatchLength(byte[] source, int current, int candidate, int srcEnd)
    {
        int length = 0;
        while (current + length < srcEnd && candidate + length < srcEnd && source[current + length] == source[candidate + length])
        {
            length++;
        }
        return length;
    }

    /// <summary>
    /// Emits a full LZ4 sequence (literals + match) into the destination buffer.
    /// Returns the new dest index, or -1 on overflow.
    /// </summary>
    private static int EmitSequence(byte[] dest, int destIndex, byte[] source, int literalStart, int literalLength, int matchOffset, int matchLength)
    {
        int adjustedMatchLength = matchLength - MinMatch;

        // Write token byte.
        int tokenIndex = destIndex++;
        if (tokenIndex >= dest.Length)
            return -1;

        int tokenLiteral = Math.Min(literalLength, 15);
        int tokenMatch = Math.Min(adjustedMatchLength, 15);
        dest[tokenIndex] = (byte)((tokenLiteral << 4) | tokenMatch);

        // Write extended literal length.
        if (literalLength >= 15)
        {
            int remaining = literalLength - 15;
            while (remaining >= 255)
            {
                if (destIndex >= dest.Length)
                    return -1;
                dest[destIndex++] = 255;
                remaining -= 255;
            }
            if (destIndex >= dest.Length)
                return -1;
            dest[destIndex++] = (byte)remaining;
        }

        // Copy literal bytes.
        if (destIndex + literalLength > dest.Length)
            return -1;
        Buffer.BlockCopy(source, literalStart, dest, destIndex, literalLength);
        destIndex += literalLength;

        // Write match offset (2 bytes, little-endian).
        if (destIndex + 2 > dest.Length)
            return -1;
        dest[destIndex++] = (byte)(matchOffset & 0xFF);
        dest[destIndex++] = (byte)((matchOffset >> 8) & 0xFF);

        // Write extended match length.
        if (adjustedMatchLength >= 15)
        {
            int remaining = adjustedMatchLength - 15;
            while (remaining >= 255)
            {
                if (destIndex >= dest.Length)
                    return -1;
                dest[destIndex++] = 255;
                remaining -= 255;
            }
            if (destIndex >= dest.Length)
                return -1;
            dest[destIndex++] = (byte)remaining;
        }

        return destIndex;
    }

    /// <summary>
    /// Emits a literal-only sequence (final sequence with no match).
    /// Returns the new dest index, or -1 on overflow.
    /// </summary>
    private static int EmitLiteralsOnly(byte[] dest, int destIndex, byte[] source, int literalStart, int literalLength)
    {
        // Token with literal length, match nibble = 0 (no match follows).
        int tokenIndex = destIndex++;
        if (tokenIndex >= dest.Length)
            return -1;

        int tokenLiteral = Math.Min(literalLength, 15);
        dest[tokenIndex] = (byte)(tokenLiteral << 4);

        // Extended literal length.
        if (literalLength >= 15)
        {
            int remaining = literalLength - 15;
            while (remaining >= 255)
            {
                if (destIndex >= dest.Length)
                    return -1;
                dest[destIndex++] = 255;
                remaining -= 255;
            }
            if (destIndex >= dest.Length)
                return -1;
            dest[destIndex++] = (byte)remaining;
        }

        // Copy literal bytes.
        if (destIndex + literalLength > dest.Length)
            return -1;
        Buffer.BlockCopy(source, literalStart, dest, destIndex, literalLength);
        destIndex += literalLength;

        return destIndex;
    }
}
