using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ClojureCollectionsCLR
{
    public class PersistentList<T> : IPersistentList<T>
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

        public bool Equiv(IPersistentList<T> list)
        {
            var cList = list as PersistentList<T>;
            if (cList == null)
                return false;

            return _clojureList.equiv(cList._clojureList);
        }

        public T Peek()
        {
            return (T)_clojureList.peek();
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

        public override bool Equals(object obj)
        {
            if (obj is IPersistentList<T>)
                return Equiv(obj as IPersistentList<T>);

            return false;
        }

        public override int GetHashCode()
        {
            return _clojureList.GetHashCode();
        }

        public override string ToString()
        {
            return _clojureList.ToString();
        }
    }
}
