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

        public PersistentHashMap(IDictionary<K, V> dict)
        {
            _clojureMap = clojure.lang.PersistentHashMap.create(dict as IDictionary);
        }

        private PersistentHashMap(clojure.lang.IPersistentMap clojureMap)
        {
            _clojureMap = clojureMap;
        }

        public int Count { get { return _clojureMap.count(); } }

        public IPersistentMap<K, V> Cons(IMapEntry<K, V> entry)
        {
            return new PersistentHashMap<K, V>((clojure.lang.IPersistentMap)
                            _clojureMap.cons(new clojure.lang.MapEntry(entry.Key, entry.Val)));
        }

        public IPersistentMap<K, V> Empty()
        {
            return new PersistentHashMap<K, V>((clojure.lang.IPersistentMap)_clojureMap.empty());
        }

        public bool Equiv(IPersistentMap<K, V> map)
        {
            if (!(map is IPersistentMap<K, V>))
            {
                return false;
            }

            PersistentHashMap<K, V> cMap = map as PersistentHashMap<K, V>;
            return _clojureMap.equiv(cMap._clojureMap);
        }

        public bool ContiansKey(K key)
        {
            return _clojureMap.containsKey(key);
        }

        public IMapEntry<K, V> EntryAt(K key)
        {
            clojure.lang.IMapEntry clojureMapEntry = _clojureMap.entryAt(key);
            if (clojureMapEntry != null)
            {
                return new MapEntry<K, V>(clojureMapEntry);
            }
            return null;
        }

        public V ValAt(K key)
        {
            return (V)_clojureMap.valAt(key);
        }

        public V ValAt(K key, V notFound)
        {
            if (_clojureMap.containsKey(key))
            {
                return (V)_clojureMap.valAt(key);
            }
            return notFound;
        }

        public IPersistentMap<K, V> Assoc(K key, V value)
        {
            return new PersistentHashMap<K, V>(_clojureMap.assoc(key, value));
        }

        public IPersistentMap<K, V> AssocEx(K key, V value)
        {
            return new PersistentHashMap<K, V>(_clojureMap.assocEx(key, value));
        }

        public IPersistentMap<K, V> Without(K key)
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

        public override bool Equals(object obj)
        {
            if (obj is IPersistentMap<K, V>)
                return Equiv(obj as IPersistentMap<K, V>);

            return false;
        }

        public override int GetHashCode()
        {
            return _clojureMap.GetHashCode();
        }

        public override string ToString()
        {
            return _clojureMap.ToString();
        }
    }
}
