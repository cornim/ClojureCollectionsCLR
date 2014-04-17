using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace ClojureCollectionsCLR
{
    [Serializable]
    public abstract class ImmutableObject<T>
    {
        private string Hash { get { return _hash ?? (_hash = ComputeHash()); }}
        private string _hash;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is ImmutableObject<T>)
                return Hash == (obj as ImmutableObject<T>).Hash;

            return false;
        }

        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }

        public static bool operator ==(ImmutableObject<T> a, ImmutableObject<T> b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (ReferenceEquals(a, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ImmutableObject<T> a, ImmutableObject<T> b)
        {
            return !(a == b);
        }

// ReSharper disable StaticFieldInGenericType
        private static readonly HashAlgorithm CryptoServiceProvider = new MD5CryptoServiceProvider();
// ReSharper restore StaticFieldInGenericType

        private string ComputeHash()
        {
            var byteArray = ObjectToByteArray(this);
            byte[] hash;
            lock (CryptoServiceProvider)
                hash = CryptoServiceProvider.ComputeHash(byteArray);
            return Convert.ToBase64String(hash);
        }

        private static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

}
