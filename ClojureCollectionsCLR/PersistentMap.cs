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

        public IPersistentMap<TK, TV> Cons(KeyValuePair<TK, TV> entry)
        {
            return new PersistentHashMap<TK, TV>(
                            _clojureMap.cons(new clojure.lang.MapEntry(entry.Key, entry.Value)));
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

        public KeyValuePair<TK, TV> EntryAt(TK key)
        {
            if (ContainsKey(key))
            {
                clojure.lang.IMapEntry clojureMapEntry = _clojureMap.entryAt(key);
                return new KeyValuePair<TK, TV>((TK) clojureMapEntry.key(), (TV) clojureMapEntry.val());
            }
            throw new KeyNotFoundException(string.Format("Key {0} was not found.", key));
        }

        public TV ValAt(TK key)
        {
            if (ContainsKey(key))
                return (TV)_clojureMap.valAt(key);

            throw new KeyNotFoundException(string.Format("Key {0} was not found.", key));
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


        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            return new KeyValueEnumerator(_clojureMap.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class KeyValueEnumerator : IEnumerator<KeyValuePair<TK, TV>>
        {
            private readonly IEnumerator<clojure.lang.IMapEntry> _enumerator;

            public KeyValueEnumerator(IEnumerator<clojure.lang.IMapEntry> enumerator)
            {
                _enumerator = enumerator;
            }

            public KeyValuePair<TK, TV> Current
            {
                get { return new KeyValuePair<TK, TV>((TK) _enumerator.Current.key(), (TV) _enumerator.Current.val()); }
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
