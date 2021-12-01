using System;
using PseudoEnumerableTask.Interfaces;

namespace Predicates
{
    /// <summary>
    /// ContainsDigitPredicate.
    /// </summary>
    public class ContainsDigitPredicate : IPredicate<int>
    {
        /// <summary>
        /// Gets or sets the digit.
        /// </summary>
        /// <value>
        /// The digit.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value - Digit value is out of range (0..9).</exception>
        public int Digit
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Represents the method that defines a set of criteria and determines whether the specified object meets those criteria.
        /// </summary>
        /// <param name="obj">The object to compare against the criteria.</param>
        /// <returns>
        /// true if obj meets the criteria defined within the method; otherwise, false.
        /// </returns>
        public bool Verify(int obj)
        {
            throw new NotImplementedException();
        }
    }
}
