using System;
using PseudoEnumerableTask.Interfaces;

namespace Transformers
{
    /// <summary>
    /// GetIEEE754FormatAdapter.
    /// </summary>
    /// <seealso>
    ///     <cref>PseudoEnumerableTask.Interfaces.ITransformer&amp;lt;double, string&amp;gt;</cref>
    /// </seealso>
    public class Ieee754FormatTransformer : ITransformer<double, string>
    {
        /// <summary>
        /// Represents a method that converts an object from one type to another type.
        /// </summary>
        /// <param name="number">The object to convert.</param>
        /// <returns>
        /// The TResult that represents the converted TSource.
        /// </returns>
        public string Transform(double number)
        {
            throw new NotImplementedException();
        }
    }
}
