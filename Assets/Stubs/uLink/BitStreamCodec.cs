// Stub: uLink.BitStreamCodec - registers custom type serializers for BitStream
// All registration methods are no-ops in single-player.

using System;

namespace uLink
{
    public static class BitStreamCodec
    {
        public delegate void Serializer<T>(BitStream stream, object value, params object[] codecOptions);
        public delegate object Deserializer<T>(BitStream stream, params object[] codecOptions);

        /// <summary>Register a custom codec for type T. No-op in single-player.</summary>
        public static void Add<T>(Serializer<T> serializer, Deserializer<T> deserializer) { }

        /// <summary>Register a custom codec for type T. No-op in single-player.</summary>
        public static void Add<T>(Action<BitStream, T> serializer, Func<BitStream, T> deserializer) { }

        /// <summary>Register a custom codec and array codec for type T. No-op in single-player.</summary>
        public static void AddAndMakeArray<T>(Action<BitStream, T> serializer, Func<BitStream, T> deserializer) { }

        /// <summary>Register a custom codec for a specific type. No-op in single-player.</summary>
        public static void Add(Type type, Delegate serializer, Delegate deserializer) { }
    }
}
