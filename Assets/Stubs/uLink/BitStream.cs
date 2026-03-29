// Stub: uLink.BitStream - network serialization stream (681 references in codebase)
// All Read methods return default values. All Write methods are no-ops.

using UnityEngine;

namespace uLink
{
    public class BitStream
    {
        /// <summary>True when reading data from the stream.</summary>
        public bool isReading { get; set; }

        /// <summary>True when writing data to the stream.</summary>
        public bool isWriting { get; set; }

        /// <summary>Number of bits read so far.</summary>
        public int bitsRead { get { return 0; } }

        /// <summary>Number of bits written so far.</summary>
        public int bitsWritten { get { return 0; } }

        // ---- Generic Read/Write ----

        public T Read<T>() { return default(T); }
        public bool TryRead<T>(out T value) { value = default(T); return false; }
        public void Write<T>(T value) { }

        // ---- Primitive Read Methods ----

        public bool ReadBoolean() { return false; }
        public byte ReadByte() { return 0; }
        public byte[] ReadBytes() { return new byte[0]; }
        public byte[] ReadBytes(int count) { return new byte[count < 0 ? 0 : count]; }
        public sbyte ReadSByte() { return 0; }
        public char ReadChar() { return '\0'; }
        public short ReadInt16() { return 0; }
        public ushort ReadUInt16() { return 0; }
        public int ReadInt32() { return 0; }
        public uint ReadUInt32() { return 0; }
        public long ReadInt64() { return 0; }
        public ulong ReadUInt64() { return 0; }
        public float ReadSingle() { return 0f; }
        public double ReadDouble() { return 0.0; }
        public string ReadString() { return string.Empty; }

        // ---- Unity Type Read Methods ----

        public Vector2 ReadVector2() { return Vector2.zero; }
        public Vector3 ReadVector3() { return Vector3.zero; }
        public Vector4 ReadVector4() { return Vector4.zero; }
        public Quaternion ReadQuaternion() { return Quaternion.identity; }
        public Color ReadColor() { return Color.white; }
        public Color32 ReadColor32() { return new Color32(255, 255, 255, 255); }
        public Rect ReadRect() { return new Rect(); }

        // ---- uLink Type Read Methods ----

        public NetworkPlayer ReadNetworkPlayer() { return new NetworkPlayer(); }
        public NetworkViewID ReadNetworkViewID() { return NetworkViewID.unassigned; }

        // ---- Primitive Write Methods ----

        public void WriteBoolean(bool value) { }
        public void WriteByte(byte value) { }
        public void WriteBytes(byte[] value) { }
        public void WriteBytes(byte[] value, int offset, int count) { }
        public void WriteSByte(sbyte value) { }
        public void WriteChar(char value) { }
        public void WriteInt16(short value) { }
        public void WriteUInt16(ushort value) { }
        public void WriteInt32(int value) { }
        public void WriteUInt32(uint value) { }
        public void WriteInt64(long value) { }
        public void WriteUInt64(ulong value) { }
        public void WriteSingle(float value) { }
        public void WriteDouble(double value) { }
        public void WriteString(string value) { }

        // ---- Unity Type Write Methods ----

        public void WriteVector2(Vector2 value) { }
        public void WriteVector3(Vector3 value) { }
        public void WriteVector4(Vector4 value) { }
        public void WriteQuaternion(Quaternion value) { }
        public void WriteColor(Color value) { }
        public void WriteColor32(Color32 value) { }
        public void WriteRect(Rect value) { }

        // ---- uLink Type Write Methods ----

        public void WriteNetworkPlayer(NetworkPlayer value) { }
        public void WriteNetworkViewID(NetworkViewID value) { }
    }
}
