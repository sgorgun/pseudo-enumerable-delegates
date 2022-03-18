using System;
using PseudoEnumerableTask.Interfaces;

namespace Transformers
{
    /// <summary>
    /// Transformer class.
    /// </summary>
    /// <seealso cref="ITransformer{TSource,TResult}" />
    public class StringLengthTransformer : ITransformer<string, int>
    {
        /// <summary>
        /// Represents a method that converts an object from one type to another type.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>
        /// The TResult that represents the converted TSource.
        /// </returns>
        public int Transform(string? obj)
        {
            throw new NotImplementedException();
        }
    }
}
