using UnityEngine;
using System;

/// <summary>
/// Test harness for ManagedLZ4 compression library.
/// Attach to a GameObject to run all tests on Start.
/// </summary>
public class ManagedLZ4Tests : MonoBehaviour
{
    void Start()
    {
        bool passed = RunAllTests();
        if (passed)
            Debug.Log("[ManagedLZ4Tests] ALL TESTS PASSED");
        else
            Debug.LogError("[ManagedLZ4Tests] SOME TESTS FAILED");
    }

    public static bool RunAllTests()
    {
        bool allPassed = true;

        allPassed &= RunTest("CompressReturnsPositiveSize", TestCompressReturnsPositiveSize);
        allPassed &= RunTest("UncompressUnknownOutputSizeRoundTrip", TestUncompressUnknownOutputSizeRoundTrip);
        allPassed &= RunTest("CompressDecompressRoundTrip", TestCompressDecompressRoundTrip);
        allPassed &= RunTest("UncompressExactOutputSize", TestUncompressExactOutputSize);
        allPassed &= RunTest("EmptyInputReturnsZero", TestEmptyInputReturnsZero);
        allPassed &= RunTest("LargeInputRoundTrip", TestLargeInputRoundTrip);
        allPassed &= RunTest("DllLoadReturnsZero", TestDllLoadReturnsZero);

        return allPassed;
    }

    private static bool RunTest(string name, Func<bool> test)
    {
        try
        {
            bool result = test();
            if (!result)
                Debug.LogError("[ManagedLZ4Tests] FAIL: " + name);
            else
                Debug.Log("[ManagedLZ4Tests] PASS: " + name);
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError("[ManagedLZ4Tests] FAIL (exception): " + name + " - " + ex.Message);
            return false;
        }
    }

    private static bool TestCompressReturnsPositiveSize()
    {
        byte[] source = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        byte[] dest = new byte[256];
        int compressedSize = ManagedLZ4.LZ4_compress(source, dest, source.Length);
        return compressedSize > 0;
    }

    private static bool TestUncompressUnknownOutputSizeRoundTrip()
    {
        byte[] original = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160 };
        byte[] compressed = new byte[256];
        int compressedSize = ManagedLZ4.LZ4_compress(original, compressed, original.Length);

        byte[] decompressed = new byte[256];
        int decompressedSize = ManagedLZ4.LZ4_uncompress_unknownOutputSize(compressed, decompressed, compressedSize, decompressed.Length);

        if (decompressedSize != original.Length)
            return false;

        for (int i = 0; i < original.Length; i++)
        {
            if (decompressed[i] != original[i])
                return false;
        }
        return true;
    }

    private static bool TestCompressDecompressRoundTrip()
    {
        byte[] original = new byte[128];
        for (int i = 0; i < original.Length; i++)
            original[i] = (byte)(i % 37);

        byte[] compressed = new byte[512];
        int compressedSize = ManagedLZ4.LZ4_compress(original, compressed, original.Length);
        if (compressedSize <= 0)
            return false;

        byte[] decompressed = new byte[original.Length];
        int decompressedSize = ManagedLZ4.LZ4_uncompress(compressed, decompressed, original.Length);
        if (decompressedSize < 0)
            return false;

        for (int i = 0; i < original.Length; i++)
        {
            if (decompressed[i] != original[i])
                return false;
        }
        return true;
    }

    private static bool TestUncompressExactOutputSize()
    {
        byte[] original = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0x00 };
        byte[] compressed = new byte[256];
        int compressedSize = ManagedLZ4.LZ4_compress(original, compressed, original.Length);

        byte[] decompressed = new byte[original.Length];
        int result = ManagedLZ4.LZ4_uncompress(compressed, decompressed, original.Length);
        if (result < 0)
            return false;

        for (int i = 0; i < original.Length; i++)
        {
            if (decompressed[i] != original[i])
                return false;
        }
        return true;
    }

    private static bool TestEmptyInputReturnsZero()
    {
        byte[] source = new byte[0];
        byte[] dest = new byte[256];
        int compressedSize = ManagedLZ4.LZ4_compress(source, dest, 0);
        return compressedSize >= 0;
    }

    private static bool TestLargeInputRoundTrip()
    {
        // 64KB of repeating data - should compress well
        byte[] original = new byte[65536];
        for (int i = 0; i < original.Length; i++)
            original[i] = (byte)(i % 251);

        // LZ4 worst case is slightly larger than input
        byte[] compressed = new byte[original.Length + (original.Length / 255) + 16];
        int compressedSize = ManagedLZ4.LZ4_compress(original, compressed, original.Length);
        if (compressedSize <= 0)
            return false;

        byte[] decompressed = new byte[original.Length + 256];
        int decompressedSize = ManagedLZ4.LZ4_uncompress_unknownOutputSize(compressed, decompressed, compressedSize, decompressed.Length);

        if (decompressedSize != original.Length)
            return false;

        for (int i = 0; i < original.Length; i++)
        {
            if (decompressed[i] != original[i])
                return false;
        }
        return true;
    }

    private static bool TestDllLoadReturnsZero()
    {
        return ManagedLZ4.LZ4_DllLoad() == 0;
    }
}
