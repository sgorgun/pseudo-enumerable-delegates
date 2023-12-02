using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PseudoEnumerableTask.Tests")]

namespace PseudoEnumerableTask
{
    /// <summary>
    /// Implements array helper methods.
    /// </summary>
    internal static class BufferData
    {
        /// <summary>
        /// Creates array on base of enumerable sequence.
        /// </summary>
        /// <param name="source">The enumerable sequence.</param>
        /// <typeparam name="T">Type of the elements of the sequence.</typeparam>
        /// <returns>Single dimension zero based array and count of elements of array.</returns>
        /// <exception cref="ArgumentNullException">Throw when source is null.</exception>
        internal static (T[] buffer, int count) ToArray<T>(IEnumerable<T> source)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source), "Sequence can not be null.");

            if (source is ICollection<T> collection)
            {
                var array = new T[collection.Count];
                collection.CopyTo(array, 0);
                return (array, array.Length);
            }

            var buffer = new T[4];
            var count = 0;

            foreach (var item in source!)
            {
                if (count == buffer.Length)
                {
                    Array.Resize(ref buffer, buffer.Length * 2);
                }

                buffer[count++] = item;
            }

            return (count == 0 ? Array.Empty<T>() : buffer, count);
        }
    }
}
