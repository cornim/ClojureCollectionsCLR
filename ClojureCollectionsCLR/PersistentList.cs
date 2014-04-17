using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ClojureCollectionsCLR
{
    [Serializable]
    public class PersistentList<T> : ImmutableObject<PersistentList<T>>, IPersistentList<T>
    {

        private readonly clojure.lang.IPersistentList _clojureList;

        public PersistentList()
        {
            _clojureList = clojure.lang.PersistentList.create(new List<T>());
        }

        public PersistentList(IList<T> list)
        {
            _clojureList = clojure.lang.PersistentList.create((IList)list);
        }

        private PersistentList(clojure.lang.IPersistentList clojureList)
        {
            _clojureList = clojureList;
        }

        public int Count { get { return _clojureList.count(); } }

        public IPersistentList<T> Cons(T o)
        {
            return new PersistentList<T>((clojure.lang.IPersistentList)_clojureList.cons(o));
        }

        public IPersistentList<T> Empty()
        {
            return new PersistentList<T>((clojure.lang.IPersistentList)_clojureList.empty());
        }

        [Obsolete("Use regular equals instead.")]
        public bool Equiv(IPersistentList<T> list)
        {
            return Equals(list);
        }

        public T Peek()
        {
            if (Count > 0)
                return (T)_clojureList.peek();

            throw new InvalidOperationException("Can't peek on empty list.");
        }

        public IPersistentList<T> Pop()
        {
            return new PersistentList<T>((clojure.lang.IPersistentList)_clojureList.pop());
        }

        public IPersistentList<T> Without(T item)
        {
            IPersistentList<T> ret = new PersistentList<T>();

            ret = this.Where(t => !(t.Equals(item))).Aggregate(ret, (current, t) => current.Cons(t));

            return ret;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SeqEnumerator<T>(new clojure.lang.SeqEnumerator(_clojureList.seq()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return _clojureList.ToString();
        }
    }
}
