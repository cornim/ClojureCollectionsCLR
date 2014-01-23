using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ClojureCollectionsCLR
{
    public class PersistentHashMap<K, V> : IPersistentMap<K, V>
    {
        private readonly clojure.lang.IPersistentMap _clojureMap;

        public PersistentHashMap()
        {
            _clojureMap = clojure.lang.PersistentHashMap.create(new Dictionary<K, V>());
        }

        public PersistentHashMap(IDictionary<K, V> map)
        {
            _clojureMap = clojure.lang.PersistentHashMap.create(map);
        }

        private PersistentHashMap(clojure.lang.IPersistentMap clojureMap)
        {
            _clojureMap = clojureMap;
        }

        public int count()
        {
            return _clojureMap.count();
        }

        public IPersistentMap<K, V> cons(IMapEntry<K, V> entry)
        {
            return new PersistentHashMap<K, V>((clojure.lang.IPersistentMap)
                            _clojureMap.cons(new clojure.lang.MapEntry(entry.key(), entry.val())));
        }

        public IPersistentMap<K, V> empty()
        {
            return new PersistentHashMap<K, V>((clojure.lang.IPersistentMap)_clojureMap.empty());
        }

        public bool equiv(IPersistentMap<K, V> map)
        {
            if (!(map is IPersistentMap<K, V>))
            {
                return false;
            }

            PersistentHashMap<K, V> cMap = map as PersistentHashMap<K, V>;
            return _clojureMap.equiv(cMap._clojureMap);
        }

        public bool contiansKey(K key)
        {
            return _clojureMap.containsKey(key);
        }

        public IMapEntry<K, V> entryAt(K key)
        {
            clojure.lang.IMapEntry clojureMapEntry = _clojureMap.entryAt(key);
            if (clojureMapEntry != null)
            {
                return new MapEntry<K, V>(clojureMapEntry);
            }
            return null;
        }

        public V valAt(K key)
        {
            return (V)_clojureMap.valAt(key);
        }

        public V valAt(K key, V notFound)
        {
            if (_clojureMap.containsKey(key))
            {
                return (V)_clojureMap.valAt(key);
            }
            return notFound;
        }

        public IPersistentMap<K, V> assoc(K key, V value)
        {
            return new PersistentHashMap<K, V>(_clojureMap.assoc(key, value));
        }

        public IPersistentMap<K, V> assocEx(K key, V value)
        {
            return new PersistentHashMap<K, V>(_clojureMap.assocEx(key, value));
        }

        public IPersistentMap<K, V> without(K key)
        {
            return new PersistentHashMap<K, V>(_clojureMap.without(key));
        }


        public IEnumerator<IMapEntry<K, V>> GetEnumerator()
        {
            return new MapEntryEnumerator(_clojureMap.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class MapEntryEnumerator : IEnumerator<IMapEntry<K, V>>
        {
            IEnumerator<clojure.lang.IMapEntry> _enumerator;

            public MapEntryEnumerator(IEnumerator<clojure.lang.IMapEntry> enumerator)
            {
                _enumerator = enumerator;
            }

            public IMapEntry<K, V> Current
            {
                get { return new MapEntry<K, V>(_enumerator.Current); }
            }

            public void Dispose()
            {
                _enumerator.Dispose();
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator.Reset();
            }
        }
    }
}
