using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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

        public int count()
        {
            return _clojureList.count();
        }

        public IPersistentList<T> cons(T o)
        {
            return new PersistentList<T>((clojure.lang.IPersistentList)_clojureList.cons(o));
        }

        public IPersistentList<T> empty()
        {
            return new PersistentList<T>((clojure.lang.IPersistentList)_clojureList.empty());
        }

        public bool equiv(IPersistentList<T> list)
        {
            if (!(list is PersistentList<T>))
            {
                return false;
            }

            PersistentList<T> cList = (PersistentList<T>)list;
            return _clojureList.equiv(cList._clojureList);
        }

        public T peek()
        {
            return (T)_clojureList.peek();
        }

        public IPersistentList<T> pop()
        {
            return new PersistentList<T>((clojure.lang.IPersistentList)_clojureList.pop());
        }

        public IPersistentList<T> without(T item)
        {
            IPersistentList<T> ret = new PersistentList<T>();
            foreach (T t in this)
            {
                if (!(t.Equals(item)))
                {
                    ret = ret.cons(t);
                }
            }
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
    }
}
