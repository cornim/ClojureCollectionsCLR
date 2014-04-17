using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ClojureCollectionsCLR
{
    [Serializable]
    public class PersistentVector<T> : ImmutableObject<PersistentVector<T>>, IPersistentVector<T>
    {
        private readonly clojure.lang.IPersistentVector _clojureVector;

        public PersistentVector()
        {
            _clojureVector = clojure.lang.PersistentVector.create1(new List<T>());
        }

        public PersistentVector(ICollection items)
        {
            _clojureVector = clojure.lang.PersistentVector.create1(items);
        }

        private PersistentVector(clojure.lang.IPersistentVector clojureVector)
        {
            _clojureVector = clojureVector;
        }

        public int Count { get { return _clojureVector.count(); } }

        public IPersistentVector<T> Empty()
        {
            return new PersistentVector<T>((clojure.lang.IPersistentVector)_clojureVector.empty());
        }

        [Obsolete("Use regular equals instead.")]
        public bool Equiv(IPersistentVector<T> vec)
        {
            return Equals(vec);
        }

        public T Peek()
        {
            if (Count > 0)
                return (T)_clojureVector.peek();

            throw new InvalidOperationException("Can't peek on empty vector.");
        }

        public IPersistentVector<T> Pop()
        {
            return new PersistentVector<T>((clojure.lang.IPersistentVector)_clojureVector.pop());
        }

        public int Length()
        {
            return _clojureVector.length();
        }

        public IPersistentVector<T> AssocN(int i, T val)
        {
            return new PersistentVector<T>(_clojureVector.assocN(i, val));
        }

        public IPersistentVector<T> Cons(T val)
        {
            return new PersistentVector<T>(_clojureVector.cons(val));
        }

        public T Nth(int n)
        {
            return (T) _clojureVector.nth(n);
        }

        public bool ContainsKey(int key)
        {
            return _clojureVector.containsKey(key);
        }

        public KeyValuePair<int, T> EntryAt(int key)
        {
            clojure.lang.IMapEntry entry = _clojureVector.entryAt((key));
            if (key >= 0 && key < Count)
                return new KeyValuePair<int, T>((int) entry.key(), (T) entry.val());
            throw new KeyNotFoundException(string.Format("Key {0} was not found.", key));
        }

        public T ValAt(int key)
        {
            if (key >= 0 && key < Count)
                return Nth(key);
            throw new IndexOutOfRangeException();
        }

        public T ValAt(int key, T notFound)
        {
            if (key >= 0 && key < Count)
                return Nth(key);
            return notFound;
        }

        public IPersistentVector<T> SubVec(int start, int end)
        {
            return new PersistentVector<T>(clojure.lang.RT.subvec(_clojureVector, start, end));
        }

        public IPersistentVector<T> Without(T item)
        {
            IPersistentVector<T> ret = new PersistentVector<T>();
            ret = this.Where(t => !(t.Equals(item))).Aggregate(ret, (current, t) => current.Cons(t));
            return ret;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SeqEnumerator<T>(new clojure.lang.SeqEnumerator(_clojureVector.seq()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return _clojureVector.ToString();
        }
    }
}
