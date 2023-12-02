using System;
using PseudoEnumerableTask.Interfaces;

namespace Predicates
{
    /// <summary>
    /// ContainsDigitPredicate.
    /// </summary>
    public class ContainsDigitPredicate : IPredicate<int>
    {
        private int digit;

        /// <summary>
        /// Gets or sets the digit.
        /// </summary>
        /// <value>
        /// The digit.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">value - Digit value is out of range (0..9).</exception>
        public int Digit
        {
            get => this.digit;
            set => this.digit = (value is < 0 or > 9) ? throw new ArgumentOutOfRangeException(nameof(value), "Сan not be less than zero or more then 9.") : value;
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
            uint newObj = obj < 0 ? (uint)-obj : (uint)obj;

            while (newObj != 0)
            {
                if (newObj % 10 == this.digit)
                {
                    return true;
                }

                newObj /= 10;
            }

            return false;
        }
    }
}
