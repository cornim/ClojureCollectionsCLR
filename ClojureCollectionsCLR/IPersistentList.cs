using System.Collections.Generic;

namespace ClojureCollectionsCLR
{
    /**Interface for a persistent (immutable) list.
    * All methods are guaranteed to leave the object itself unmodified and only
    * return modified copies of the original.
    *
    * @author Dr. Cornelius Mund
    *
    * @param <T> Type of items to be contained in the list. */
    public interface IPersistentList<T> : IEnumerable<T>
    {
        /**Returns the number of objects currently in the list.*/
        int Count { get; }

        /**Adds an object of type T to the List and returns it.
         *
         * @param o Object to be added.
         * @return New list with the additional object added.*/
        IPersistentList<T> Cons(T o);

        /**Returns the list with all elements removed.*/
        IPersistentList<T> Empty();

        /**Checks if this list is equivalent to another list.
         * Equivalent means it has equal items at the same positions.
         *
         * @param list The other list to compare with.
         * @return True if the two lists are equivalent, False otherwise.*/
        bool Equiv(IPersistentList<T> list);

        /**Returns the object which was last added to the list.*/
        T Peek();

        /**Returns a list where the object which was added last has been removed.*/
        IPersistentList<T> Pop();

        /**Returns a list where the item specified has been removed.
         * If no item in the list is equal the the item specified the entire list
         * is returned.
         *
         * @param item The item to be removed.
         * @return The list with the item specified removed. */
        IPersistentList<T> Without(T item);
    }
}
