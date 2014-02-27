using System.Collections.Generic;
using System.Collections;

namespace ClojureCollectionsCLR
{
    public class PersistentHashMap<TK, TV> : IPersistentMap<TK, TV>
    {
        private readonly clojure.lang.IPersistentMap _clojureMap;

        public PersistentHashMap()
        {
            _clojureMap = clojure.lang.PersistentHashMap.create(new Dictionary<TK, TV>());
        }

        public PersistentHashMap(IDictionary<TK, TV> dict)
        {
            _clojureMap = clojure.lang.PersistentHashMap.create(dict as IDictionary);
        }

        private PersistentHashMap(clojure.lang.IPersistentMap clojureMap)
        {
            _clojureMap = clojureMap;
        }

        public int Count { get { return _clojureMap.count(); } }

        public IPersistentMap<TK, TV> Cons(IMapEntry<TK, TV> entry)
        {
            return new PersistentHashMap<TK, TV>(
                            _clojureMap.cons(new clojure.lang.MapEntry(entry.Key, entry.Val)));
        }

        public IPersistentMap<TK, TV> Empty()
        {
            return new PersistentHashMap<TK, TV>((clojure.lang.IPersistentMap)_clojureMap.empty());
        }

        public bool Equiv(IPersistentMap<TK, TV> map)
        {
            if (map == null)
                return false;

            var cMap = map as PersistentHashMap<TK, TV>;
            if (cMap == null)
                return false;
            return _clojureMap.equiv(cMap._clojureMap);
        }

        public bool ContainsKey(TK key)
        {
            return _clojureMap.containsKey(key);
        }

        public IMapEntry<TK, TV> EntryAt(TK key)
        {
            clojure.lang.IMapEntry clojureMapEntry = _clojureMap.entryAt(key);
            if (clojureMapEntry != null)
            {
                return new MapEntry<TK, TV>(clojureMapEntry);
            }
            return null;
        }

        public TV ValAt(TK key)
        {
            return (TV)_clojureMap.valAt(key);
        }

        public TV ValAt(TK key, TV notFound)
        {
            if (_clojureMap.containsKey(key))
            {
                return (TV)_clojureMap.valAt(key);
            }
            return notFound;
        }

        public IPersistentMap<TK, TV> Assoc(TK key, TV value)
        {
            return new PersistentHashMap<TK, TV>(_clojureMap.assoc(key, value));
        }

        public IPersistentMap<TK, TV> AssocEx(TK key, TV value)
        {
            return new PersistentHashMap<TK, TV>(_clojureMap.assocEx(key, value));
        }

        public IPersistentMap<TK, TV> Without(TK key)
        {
            return new PersistentHashMap<TK, TV>(_clojureMap.without(key));
        }


        public IEnumerator<IMapEntry<TK, TV>> GetEnumerator()
        {
            return new MapEntryEnumerator(_clojureMap.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class MapEntryEnumerator : IEnumerator<IMapEntry<TK, TV>>
        {
            private readonly IEnumerator<clojure.lang.IMapEntry> _enumerator;

            public MapEntryEnumerator(IEnumerator<clojure.lang.IMapEntry> enumerator)
            {
                _enumerator = enumerator;
            }

            public IMapEntry<TK, TV> Current
            {
                get { return new MapEntry<TK, TV>(_enumerator.Current); }
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
            if (obj is IPersistentMap<TK, TV>)
                return Equiv(obj as IPersistentMap<TK, TV>);

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
