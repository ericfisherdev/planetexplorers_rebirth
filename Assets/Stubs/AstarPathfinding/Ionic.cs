// Stub: Pathfinding.Ionic.Zlib namespace
// Used in Assets/Scripts/ZhouXun/Voxel Creation/Scripts/Misc/Zip.cs via:
//   using Ionic = Pathfinding.Ionic;
// Provides ZlibStream with CompressionMode and CompressionLevel for voxel data compression.

using System;
using System.IO;

namespace Pathfinding.Ionic.Zlib
{
    public enum CompressionMode
    {
        Compress,
        Decompress
    }

    public enum CompressionLevel
    {
        None,
        BestSpeed,
        BestCompression,
        Default,
        Level0,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9
    }

    /// <summary>
    /// Zlib compression stream. Game code wraps source/dest streams for voxel data
    /// compression and decompression. Stub delegates to System.IO.Compression.DeflateStream.
    /// </summary>
    public class ZlibStream : Stream
    {
        private readonly Stream _baseStream;
        private readonly CompressionMode _mode;
        private readonly bool _leaveOpen;
        private readonly System.IO.Compression.DeflateStream _inner;

        public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
        {
            _baseStream = stream;
            _mode = mode;
            _leaveOpen = leaveOpen;

            var sysMode = mode == CompressionMode.Compress
                ? System.IO.Compression.CompressionMode.Compress
                : System.IO.Compression.CompressionMode.Decompress;

            _inner = new System.IO.Compression.DeflateStream(stream, sysMode, leaveOpen);
        }

        public override bool CanRead => _inner.CanRead;
        public override bool CanSeek => false;
        public override bool CanWrite => _inner.CanWrite;
        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override void Flush() => _inner.Flush();
        public override int Read(byte[] buffer, int offset, int count) => _inner.Read(buffer, offset, count);
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => _inner.Write(buffer, offset, count);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _inner.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
