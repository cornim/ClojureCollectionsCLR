namespace ClojureCollectionsCLR
{
    public class MapEntry<TK, TV> : IMapEntry<TK, TV>
    {
        private readonly TK _key;
        private readonly TV _val;

        public MapEntry(TK key, TV val)
        {
            _key = key;
            _val = val;
        }

        internal MapEntry(clojure.lang.IMapEntry clojureMapEntry)
        {
            _key = (TK)clojureMapEntry.key();
            _val = (TV)clojureMapEntry.val();
        }

        public TK Key { get { return _key; } }

        public TV Val { get { return _val; } }

    }
}
