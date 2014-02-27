﻿using System.Collections.Generic;

namespace ClojureCollectionsCLR
{
    /**Interface for a persistent (immutable) map.
    * All methods are guaranteed to leave the object itself unmodified and only
    * return modified copies of the original.
    *
    * @author Dr. Cornelius Mund
    *
    * @param <K> Key type
    * @param <V> Value type
    */
    public interface IPersistentMap<TK, TV> : IEnumerable<IMapEntry<TK, TV>>
    {
        /**Returns the number of objects currently in the map.*/
        int Count { get; }

        /**Adds MapEntry to the map.
         *
         * @param entry MapEntry to be added to the map.
         * @return New map with the additional MapEntry added.*/
        IPersistentMap<TK, TV> Cons(IMapEntry<TK, TV> entry);

        /**Returns the map with all elements removed.*/
        IPersistentMap<TK, TV> Empty();

        /**Checks if this map is equivalent to another map.
         * Equivalent means it has the same number of entries and
         * equal values under equal keys.
         *
         * @param map The other map to compare with.
         * @return True if the two vectors are equivalent, False otherwise.*/
        bool Equiv(IPersistentMap<TK, TV> map);

        /**Contains true if the map contains key as a key, false otherwise.*/
        bool ContainsKey(TK key);

        /**Returns the map entry for key or null if key is not found in the map.*/
        IMapEntry<TK, TV> EntryAt(TK key);

        /**Returns the value for key or null if key is not found in the map.*/
        TV ValAt(TK key);

        /**Returns the value for key or notFound if key is not found in the map.
         *
         * @param key Key for which the value should be returned.
         * @param notFound Object which is returned if key is not found.*/
        TV ValAt(TK key, TV notFound);

        /**Returns a new PersistentMap where the key value pair is added to the
         * current map. If key was already present in the map the corresponding
         * value is overwritten.*/
        IPersistentMap<TK, TV> Assoc(TK key, TV value);

        /**Returns a new PersistentMap where the key value pair is added to the
         * current map. If key was already present in the map an Exception is thrown.*/
        IPersistentMap<TK, TV> AssocEx(TK key, TV value);

        /**Returns the current map without key and the corresponding value. If key
         * is not part of the map the entire map is returned.*/
        IPersistentMap<TK, TV> Without(TK key);
    }
}
