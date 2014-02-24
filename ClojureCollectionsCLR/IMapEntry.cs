namespace ClojureCollectionsCLR
{
    public interface IMapEntry<out TK, out TV>
    {
        TK Key { get; }
        TV Val { get; }
    }
}
