using System;
using System.Runtime.InteropServices;
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
        /// <param name="obj">The object to convert.</param>
        /// <returns>
        /// The TResult that represents the converted TSource.
        /// </returns>
        public string Transform(double obj)
        {
            DoubleToLongConverter converter = new DoubleToLongConverter(obj);
            long longNumber = converter.LongTerm;
            const int bitsInByte = 8;
            const int bitsCount = sizeof(double) * bitsInByte;
            char[] result = new char[bitsCount];
            result[0] = longNumber < 0 ? '1' : '0';
            for (int i = bitsCount - 2, j = 1; i >= 0; i--, j++)
            {
                result[j] = (longNumber & (1L << i)) != 0 ? '1' : '0';
            }

            return new string(result);
        }

        [StructLayout(LayoutKind.Explicit)]
        private readonly struct DoubleToLongConverter
        {
            [field: FieldOffset(0)]
            public readonly long LongTerm;

            [FieldOffset(0)]
            private readonly double doubleTerm;

            public DoubleToLongConverter(double doubleTerm)
                : this() => this.doubleTerm = doubleTerm;
        }
    }
}
