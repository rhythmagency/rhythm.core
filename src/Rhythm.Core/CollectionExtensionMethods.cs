namespace Rhythm.Core;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Extension methods for collections.
/// </summary>
public static class CollectionExtensionMethods
{

    #region Properties

    /// <summary>
    /// Used to generate random numbers.
    /// </summary>
    private static Random Rnd { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Static constructor.
    /// </summary>
    static CollectionExtensionMethods()
    {
        Rnd = new Random();
    }

    #endregion

    #region Extension Methods

    /// <summary>
    /// Converts a null collection into an empty collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item stored by the collection.
    /// </typeparam>
    /// <param name="items">
    /// The collection of items.
    /// </param>
    /// <returns>
    /// An empty list, if the supplied collection is null; otherwise, the supplied collection.
    /// </returns>
    public static IEnumerable<T> MakeSafe<T>(this IEnumerable<T> items)
    {
        return items ?? Enumerable.Empty<T>();
    }

    /// <summary>
    /// Returns the collection of items without any nulls.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item stored by the collection.
    /// </typeparam>
    /// <param name="items">
    /// The collection of items.
    /// </param>
    /// <returns>
    /// The collection without null items.
    /// </returns>
    public static IEnumerable<T> WithoutNulls<T>(this IEnumerable<T> items)
    {
        return items.Where(x => x != null);
    }

    /// <summary>
    /// Generates a sequence that conains the specified value the specified number of times.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value to be repeated in the result sequence.
    /// </typeparam>
    /// <param name="element">
    /// The value to be repeated.
    /// </param>
    /// <param name="count">
    /// The number of times to repeat the value in the generated sequence.
    /// </param>
    /// <returns>
    /// A collection containing the specified element the specified number of times.
    /// </returns>
    public static IEnumerable<T> Repeat<T>(this T element, int count)
    {
        return Enumerable.Repeat(element, count);
    }

    /// <summary>
    /// Returns the specified collection of items in random order.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item stored by the collection.
    /// </typeparam>
    /// <param name="items">
    /// The collection of items.
    /// </param>
    /// <returns>
    /// The collection, in random order.
    /// </returns>
    public static IEnumerable<T> RandomOrder<T>(this IEnumerable<T> items)
    {
        var itemsList = items.MakeSafe().ToList();
        var randomized = new List<T>();
        var count = itemsList.Count;
        for(var i = 0; i < count; i++)
        {
            var index = Rnd.Next(itemsList.Count);
            var item = itemsList[index];
            randomized.Add(item);
            itemsList[index] = itemsList.Last();
            itemsList.RemoveAt(itemsList.Count - 1);
        }
        return randomized;
    }

    /// <summary>
    /// Returns a list for the specified collection. Attempts to return the
    /// underlying collection if it is already a list; otherwise, it converts
    /// the collection to a list.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item stored by the collection.
    /// </typeparam>
    /// <param name="items">
    /// The collection of items.
    /// </param>
    /// <returns>
    /// An empty list, if the supplied collection is null; otherwise, the
    /// supplied collection as a list.
    /// </returns>
    /// <remarks>
    /// This will never return a null (i.e., it will return an empty list
    /// rather than a null).
    /// </remarks>
    public static List<T> AsList<T>(this IEnumerable<T> items)
    {
        var list = items as List<T>;
        if (list == null)
        {
            list = items.MakeSafe().ToList();
        }
        return list;
    }

    #endregion

}
