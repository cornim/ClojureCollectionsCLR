﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public interface IPersistentMap<K, V> : IEnumerable<IMapEntry<K, V>>
    {
        /**Returns the number of objects currently in the map.*/
        int count();

        //TODO: Create MapEntry class using generics.
        /**Adds MapEntry to the map.
         *
         * @param entry MapEntry to be added to the map.
         * @return New map with the additional MapEntry added.*/
        IPersistentMap<K, V> cons(IMapEntry<K, V> entry);

        /**Returns the map with all elements removed.*/
        IPersistentMap<K, V> empty();

        /**Checks if this map is equivalent to another map.
         * Equivalent means it has the same number of entries and
         * equal values under equal keys.
         *
         * @param map The other map to compare with.
         * @return True if the two vectors are equivalent, False otherwise.*/
        bool equiv(IPersistentMap<K, V> map);

        /**Contains true if the map contains key as a key, false otherwise.*/
        bool contiansKey(K key);

        /**Returns the map entry for key or null if key is not found in the map.*/
        IMapEntry<K, V> entryAt(K key);

        /**Returns the value for key or null if key is not found in the map.*/
        V valAt(K key);

        /**Returns the value for key or notFound if key is not found in the map.
         *
         * @param key Key for which the value should be returned.
         * @param notFound Object which is returned if key is not found.*/
        V valAt(K key, V notFound);

        /**Returns a new PersistentMap where the key value pair is added to the
         * current map. If key was already present in the map the corresponding
         * value is overwritten.*/
        IPersistentMap<K, V> assoc(K key, V value);

        /**Returns a new PersistentMap where the key value pair is added to the
         * current map. If key was already present in the map an Exception is thrown.*/
        IPersistentMap<K, V> assocEx(K key, V value);

        /**Returns the current map without key and the corresponding value. If key
         * is not part of the map the entire map is returned.*/
        IPersistentMap<K, V> without(K key);
    }
}
