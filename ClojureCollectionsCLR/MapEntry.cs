using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClojureCollectionsCLR
{
    public class MapEntry<K, V> : IMapEntry<K, V>
    {
        private readonly K _key;
        private readonly V _val;

        public MapEntry(K key, V val)
        {
            _key = key;
            _val = val;
        }

        internal MapEntry(clojure.lang.IMapEntry clojureMapEntry)
        {
            _key = (K)clojureMapEntry.key();
            _val = (V)clojureMapEntry.val();
        }

        public K Key()
        {
            return _key;
        }

        public V Val()
        {
            return _val;
        }

    }
}
