using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ClojureCollectionsCLR
{
    class SeqEnumerator<T> : IEnumerator<T>
    {
        private clojure.lang.SeqEnumerator _clojureEnumerator;

        public SeqEnumerator(clojure.lang.SeqEnumerator clojureEnumerator)
        {
            _clojureEnumerator = clojureEnumerator;
        }

        public T Current
        {
            get { return (T)_clojureEnumerator.Current; }
        }

        public void Dispose()
        {
            _clojureEnumerator.Dispose();
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            return _clojureEnumerator.MoveNext();
        }

        public void Reset()
        {
            _clojureEnumerator.Reset();
        }
    }
}
