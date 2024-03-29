﻿#pragma warning disable S3267
using System;
using System.Collections;
using System.Collections.Generic;
using PseudoEnumerableTask.Interfaces;

namespace PseudoEnumerableTask
{
    /// <summary>
    /// Enumerable Sequences.
    /// </summary>
    public static class EnumerableSequences
    {
        /// <summary>
        /// Filters a sequence based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="predicate">A <see cref="IPredicate{T}"/> to test each element of a sequence for a condition.</param>
        /// <returns>An sequence of elements from the source sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when source sequence or predicate is null.</exception>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source, IPredicate<TSource> predicate)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            _ = predicate ?? throw new ArgumentNullException(nameof(predicate), "Predicate can not be null.");

            return FilterIterator();

            IEnumerable<TSource> FilterIterator()
            {
                foreach (var item in source)
                {
                    if (predicate.Verify(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// Filters a sequence based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="predicate">A <see cref="Predicate{T}"/> to test each element of a sequence for a condition.</param>
        /// <returns>An sequence of elements from the source sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException">Thrown when source sequence or predicate is null.</exception>
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            _ = predicate ?? throw new ArgumentNullException(nameof(predicate), "Predicate can not be null.");

            return Filter(source, new PredicateAdapter<TSource>(predicate));
        }

        /// <summary>
        /// Transforms each element of source sequence from one type to another type by some rule.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements of result sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="transformer">A <see cref="ITransformer{TSource,TResult}"/> that defines the rule of transformation.</param>
        /// <returns>A sequence, each element of which is transformed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence or transformer is null.</exception>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source, ITransformer<TSource, TResult> transformer)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            _ = transformer ?? throw new ArgumentNullException(nameof(transformer), "Transformer can not be null.");

            return Transform(source, transformer.Transform);
        }

        /// <summary>
        /// Transforms each element of source sequence from one type to another type by some rule.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements of result sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="converter">A <see cref="Converter{TSource,TResult}"/> that defines the rule of transformation.</param>
        /// <returns>A sequence, each element of which is transformed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence or transformer is null.</exception>
        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source, Converter<TSource, TResult> converter)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            _ = converter ?? throw new ArgumentNullException(nameof(converter), "Converter can not be null.");

            return TransformIterator();

            IEnumerable<TResult> TransformIterator()
            {
                foreach (var item in source)
                {
                    yield return converter(item);
                }
            }
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        /// <returns>An ordered by comparer sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when comparer is null, and one or more elements
        /// of the sequence do not implement the <see cref="IComparable{T}"/>  interface.
        /// </exception>
        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source), "Sequence can not be null.");
            _ = comparer ?? throw new ArgumentNullException(nameof(comparer), "Comparer can not be null.");

            return SortByIterator();

            IEnumerable<TSource> SortByIterator()
            {
                TSource[] array = BufferData.ToArray(source).buffer;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - (i + 1); j++)
                    {
                        if (comparer.Compare(array[j], array[j + 1]) > 0)
                        {
                            Swap(ref array[j], ref array[j + 1]);
                        }
                    }
                }

                for (int i = 0; i < array.Length; i++)
                {
                    yield return array[i];
                }
            }
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="comparer">An <see cref="Comparison{T}"/> to compare keys.</param>
        /// <returns>An ordered by comparer sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown when comparer is null, and one or more elements
        /// of the sequence do not implement the <see cref="IComparable{T}"/>  interface.
        /// </exception>
        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source, Comparison<TSource> comparer) => SortBy(source, Comparer<TSource>.Create(comparer));

        /// <summary>
        /// Filters the elements of source sequence based on a specified type.
        /// </summary>
        /// <typeparam name="TResult">Type selector to return.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A sequence that contains the elements from source sequence that have type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        public static IEnumerable<TResult> TypeOf<TResult>(this IEnumerable source)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source), "Sequence can not be null.");

            return TypeIterator();

            IEnumerable<TResult> TypeIterator()
            {
                foreach (var item in source)
                {
                    if (item is TResult result)
                    {
                        yield return result;
                    }
                }
            }
        }

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of sequence.</typeparam>
        /// <param name="source">A sequence of elements to reverse.</param>
        /// <exception cref="ArgumentNullException">Thrown when sequence is null.</exception>
        /// <returns>Reversed source.</returns>
        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source), "Sequence can not be null.");

            return ReverseIterator();

            IEnumerable<TSource> ReverseIterator()
            {
                TSource[] array = BufferData.ToArray(source).buffer;

                for (int i = 0, j = array.Length - 1; i < j; i++, j--)
                {
                    Swap(ref array[i], ref array[j]);
                }

                for (int i = 0; i < array.Length; i++)
                {
                    yield return array[i];
                }
            }
        }

        /// <summary>
        /// Swaps two objects.
        /// </summary>
        /// <typeparam name="T">The type of parameters.</typeparam>
        /// <param name="left">First object.</param>
        /// <param name="right">Second object.</param>
        internal static void Swap<T>(ref T left, ref T right) => (left, right) = (right, left);
    }
}
