using System;
using PseudoEnumerableTask.Interfaces;

namespace PseudoEnumerableTask
{
    /// <summary>
    /// Adapter from interfase to Delegate.
    /// </summary>
    /// <typeparam name="TSource">Source.</typeparam>
    internal class PredicateAdapter<TSource> : IPredicate<TSource>
    {
        private readonly Predicate<TSource> predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateAdapter{TSourse}"/> class.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        public PredicateAdapter(Predicate<TSource> predicate)
        {
            _ = predicate ?? throw new ArgumentNullException(nameof(predicate), "Predicate can not be null.");
            this.predicate = predicate;
        }

        /// <summary>
        /// Vwerify.
        /// </summary>
        /// <param name="obj">.</param>
        /// <returns>Verification result.</returns>
        public bool Verify(TSource obj)
        {
            return this.predicate(obj);
        }
    }
}
