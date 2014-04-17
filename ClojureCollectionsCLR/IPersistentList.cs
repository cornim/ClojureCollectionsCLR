using System;
using System.Collections.Generic;

namespace ClojureCollectionsCLR
{
    /// <summary>
    /// Interface for a persistent (immutable) list.
    /// All methods are guaranteed to leave the object itself unmodified and only
    /// return modified copies of the original.
    /// </summary>
    /// <typeparam name="T">Type of items to be contained in the list.</typeparam>
    public interface IPersistentList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Returns the number of objects currently in the list.
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Adds an object of type T to the List and returns it.
        /// </summary>
        /// <param name="o">Object to be added.</param>
        /// <returns>New list with the additional object added.</returns>
        IPersistentList<T> Cons(T o);
        
        /// <summary>
        /// Returns a new list with all elements removed.
        /// </summary>
        IPersistentList<T> Empty();

        /// <summary>
        /// Checks if this list is equivalent to another list.
        /// Equivalent means it has equal items at the same positions.
        /// </summary>
        /// <param name="list">The other list to compare with.</param>
        /// <returns>True if the two lists are equivalent, False otherwise.</returns>
        [Obsolete("Use regular equals instead.")]
        bool Equiv(IPersistentList<T> list);

        /// <summary>
        /// Returns the object which was last added to the list.
        /// </summary>
        T Peek();

        /// <summary>
        /// Returns a list where the object which was added last has been removed.
        /// </summary>
        IPersistentList<T> Pop();

        /// <summary>
        /// Returns a list where the item specified has been removed.
        /// If no item in the list is equal the the item specified the entire list
        /// is returned.
        /// </summary>
        /// <param name="item">The item to be removed.</param>
        /// <returns>The list with the item specified removed.</returns>
        IPersistentList<T> Without(T item);
    }
}
