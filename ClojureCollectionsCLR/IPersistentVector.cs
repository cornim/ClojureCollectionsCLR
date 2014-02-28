using System.Collections.Generic;

namespace ClojureCollectionsCLR
{
    /// <summary>
    /// Interface for a persistent (immutable) vector.
    /// All methods are guaranteed to leave the object itself unmodified and only
    /// return modified copies of the original.
    /// </summary>
    /// 
    /// <typeparam name="T">Type of items to be contained in the vector.</typeparam>
    public interface IPersistentVector<T> : IEnumerable<T>
    {
        /// <summary>
        /// Returns the number of objects currently in the vector.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns the vector with all elements removed.
        /// </summary>
        IPersistentVector<T> Empty();

        /// <summary>
        /// Checks if this vector is equivalent to another vector.
        /// Equivalent means it has equal items at the same positions.
        /// </summary>
        /// <param name="vec">The other vector to compare with.</param>
        /// <returns>True if the two vectors are equivalent, False otherwise.</returns>
        bool Equiv(IPersistentVector<T> vec);

        /// <summary>
        /// Returns the object which was last added to the vector.
        /// </summary>
        T Peek();

        /// <summary>
        /// Returns a vector where the object which was added last has been removed.
        /// </summary>
        IPersistentVector<T> Pop();

        /// <summary>
        /// Returns the number of objects currently in the vector.
        /// </summary>
        int Length();

        /// <summary>
        /// Place a value at the i'th position in the vector thereby replacing
        /// the value which was there before. If i was not filled before then an
        /// IndexOutOfBoundsExceptions is thrown.
        /// </summary>
        /// <param name="i">Position where the new value should be placed.</param>
        /// <param name="val">The new value.</param>
        /// <returns>The new vector with the replaced value.</returns>
        IPersistentVector<T> AssocN(int i, T val);

        /// <summary>
        /// Adds an object of type T to the vector.
        /// </summary>
        /// <param name="val">Object to be added.</param>
        /// <returns>New vector with the additional object added.</returns>
        IPersistentVector<T> Cons(T val);

        /// <summary>
        /// Returns the n'th element in the list. Throws an ArgumentOutOfRange
        /// exception if there is no object at position n.
        /// </summary>
        T Nth(int n);

        /// <summary>
        /// Returns true if i is a filled element, false otherwise.
        /// </summary>
        bool ContainsKey(int i);

        /// <summary>
        /// Returns the the value at a given position together with the number of
        /// the position. If no value is present at the position a KeyNotFoundException
        /// is thrown.
        /// </summary>
        /// <param name="i">Number of the position to retrieve.</param>
        /// <returns>KeyValuePair consisting of the the position i as key and the value
        /// at position i as value.</returns>
        KeyValuePair<int, T> EntryAt(int i);

        /// <summary>
        /// Returns the object at position i. Throws an IndexOutOfRange
        /// exception if there is no object at position i.
        /// </summary>
        T ValAt(int i);

        /// <summary>
        /// Returns the object at position i or notFound if i is out of bounds.
        /// </summary>
        T ValAt(int i, T notFound);

        /// <summary>
        /// Returns a subvector of the current vector starting at start (inclusive)
        /// and ending at end (exclusive).
        /// </summary>
        /// <param name="start">Starting point (inclusive)</param>
        /// <param name="end">End point (exclusive)</param>
        IPersistentVector<T> SubVec(int start, int end);

        /// <summary>
        /// Returns the vector without item. If item is not in the vector
        /// a copy of the entire vector is returned.
        /// </summary>
        /// <param name="item">Item which is filtered from the vector.</param>
        /// <returns></returns>
        IPersistentVector<T> Without(T item);
    }
}
