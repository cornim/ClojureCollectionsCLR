using System.Collections.Generic;

namespace ClojureCollectionsCLR
{
    /// <summary>
    /// Interface for a persistent (immutable) map.
    /// All methods are guaranteed to leave the object itself unmodified and only
    /// return modified copies of the original.
    /// </summary>
    /// 
    /// <typeparam name="TK">Key type</typeparam>
    /// <typeparam name="TV">Value type</typeparam>
    public interface IPersistentMap<TK, TV> : IEnumerable<KeyValuePair<TK, TV>>
    {
        /// <summary>
        /// Returns the number of objects currently in the map.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds MapEntry to the map.
        /// </summary>
        /// 
        /// <param name="entry"> MapEntry to be added to the map.</param>
        /// <returns> New map with the additional MapEntry added.</returns>
        IPersistentMap<TK, TV> Cons(KeyValuePair<TK, TV> entry);

        /// <summary>
        /// Returns the map with all elements removed.
        /// </summary>
        IPersistentMap<TK, TV> Empty();

        /// <summary>
        /// Checks if this map is equivalent to another map.
        ///  Equivalent means it has the same number of entries and
        ///  equal values under equal keys.
        /// </summary>
        /// 
        /// <param name="map"> The other map to compare with.</param>
        /// <returns> True if the two vectors are equivalent, False otherwise.</returns>
        bool Equiv(IPersistentMap<TK, TV> map);

        /// <summary>
        /// Contains true if the map contains key as a key, false otherwise.
        /// </summary>
        bool ContainsKey(TK key);

        /// <summary>
        /// Returns the map entry for key or throws a KeyNotFoundException if key
        /// is not found in the map.
        /// </summary>
        KeyValuePair<TK, TV> EntryAt(TK key);

        /// <summary>
        /// Returns the value for key or throws a KeyNotFound exception if the key
        /// is not found in the map.
        /// </summary>
        TV ValAt(TK key);

        /// <summary>
        /// Returns the value for key or notFound if key is not found in the map.
        /// </summary>
        /// 
        /// <param name="key">Key for which the value should be returned.</param>
        /// <param name="notFound">Object which is returned if key is not found.</param>
        TV ValAt(TK key, TV notFound);

        /// <summary>
        /// Returns a new PersistentMap where the key value pair is added to the
        /// current map. If key was already present in the map the corresponding
        /// value is overwritten.
        /// </summary>
        IPersistentMap<TK, TV> Assoc(TK key, TV value);

        /// <summary>
        /// Returns a new PersistentMap where the key value pair is added to the
        /// current map. If key was already present in the map an Exception is thrown.
        /// </summary>
        IPersistentMap<TK, TV> AssocEx(TK key, TV value);

        /// <summary>
        /// Returns the current map without key and the corresponding value. If key
        /// is not part of the map the entire map is returned.
        /// </summary>
        IPersistentMap<TK, TV> Without(TK key);
    }
}
