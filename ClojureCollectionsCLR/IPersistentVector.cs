using System.Collections.Generic;

namespace ClojureCollectionsCLR
{
    /**Interface for a persistent (immutable) vector.
    * All methods are guaranteed to leave the object itself unmodified and only
    * return modified copies of the original.
    *
    * @author Dr. Cornelius Mund
    *
    * @param <T> Type of items to be contained in the vector. */
    public interface IPersistentVector<T> : IEnumerable<T>
    {
        /**Returns the number of objects currently in the vector.*/
        int Count { get; }

        /**Returns the vector with all elements removed.*/
        IPersistentVector<T> Empty();

        /**Checks if this vector is equivalent to another vector.
         * Equivalent means it has equal items at the same positions.
         *
         * @param vec The other vector to compare with.
         * @return True if the two vectors are equivalent, False otherwise.*/
        bool Equiv(IPersistentVector<T> vec);

        /**Returns the object which was last added to the vector.*/
        T Peek();

        /**Returns a vector where the object which was added last has been removed.*/
        IPersistentVector<T> Pop();

        /**Returns the number of objects currently in the vector.*/
        int Length();

        /**Place a value at the i'th position in the vector thereby replacing
         * the value which was there before. If i was not filled before then an
         * IndexOutOfBoundsExceptions is thrown.
         *
         * @param i Position where the new value should be placed.
         * @param val The new value
         * @return The new vector with the replaced value. */
        IPersistentVector<T> AssocN(int i, T val);

        /**Adds an object of type T to the vector.
         *
         * @param o Object to be added.
         * @return New vector with the additional object added.*/
        IPersistentVector<T> Cons(T val);

        /**Returns the n'th element in the list. Throws an ArgumentOutOfRange
         * exception if there is no object at position n.*/
        T Nth(int n);

        /**Returns true if i is a filled element, false otherwise.*/
        bool ContainsKey(int i);

        /**Returns the the value at a given position together with the number of
         * the position.
         *
         * @param i Number of the position to retrieve.
         * @return A MapEntry consisting of the the position i as key and the value
         * at position i as val. */
        KeyValuePair<int, T> EntryAt(int i);

        /**Returns the object at position i. Throws an IndexOutOfRange
         * exception if there is no object at position i.
         *
         * @see nth        */
        T ValAt(int i);

        /**Returns the object at position i or notFound if i is out of bounds.*/
        T ValAt(int i, T notFound);

        /**Returns a subvector of the current vector starting at start (inclusive)
         * and ending at end (exclusive). */
        IPersistentVector<T> SubVec(int start, int end);

        /**Returns the vector without item. If item is not in the vector
         * a copy of the entire vector is returned.
         *
         * @param item Item which is filtered from the vector.*/
        IPersistentVector<T> Without(T item);
    }
}
