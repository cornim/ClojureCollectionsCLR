using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ClojureCollectionsCLR
{
    public class PersistentVector<T> : IPersistentVector<T>
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

        public bool Equiv(IPersistentVector<T> vec)
        {
            if (!(vec is PersistentVector<T>))
            {
                return false;
            }

            PersistentVector<T> cVec = (PersistentVector<T>)vec;
            return _clojureVector.equiv(cVec._clojureVector);
        }

        public T Peek()
        {
            return (T)_clojureVector.peek();
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
            return (T)_clojureVector.nth(n);
        }

        public bool ContainsKey(int key)
        {
            return _clojureVector.containsKey(key);
        }

        public IMapEntry<int, T> entryAt(int key)
        {
            return new MapEntry<int, T>(_clojureVector.entryAt(key));
        }

        public T ValAt(int key)
        {
            return (T)_clojureVector.valAt(key);
        }

        public T ValAt(int key, T notFound)
        {
            if (key >= 0 && key < Count)
            {
                return Nth(key);
            }
            return notFound;
        }

        public IPersistentVector<T> SubVec(int start, int end)
        {
            return new PersistentVector<T>(clojure.lang.RT.subvec(_clojureVector, start, end));
        }

        public IPersistentVector<T> Without(T item)
        {
            IPersistentVector<T> ret = new PersistentVector<T>();
            foreach (T t in this)
            {
                if (!(t.Equals(item)))
                {
                    ret = ret.Cons(t);
                }
            }
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
    }
}
