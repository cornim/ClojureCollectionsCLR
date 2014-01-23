using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClojureCollectionsCLR
{
    public interface IMapEntry<K, V>
    {
        K key();
        V val();
    }
}
