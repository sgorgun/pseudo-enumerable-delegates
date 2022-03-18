using System;
using System.Collections.Generic;
using Comparers;
using NUnit.Framework;

namespace PseudoEnumerableTask.Tests.NUnitTests
{
    [TestFixture(
        new[] { null, "Beg", null, "Life", "I", "i", "I", null, "To" },
        new[] { null, null, null, "I", "i", "I", "To", "Beg", "Life" },
        TypeArgs = new[] { typeof(string) })]
    [TestFixture(
        new[] { 0, 12, -12, 34, 0, 2, -567, 12, -12, 89, int.MaxValue, -1000 },
        new[] { 0, 0, 2, 12, -12, 12, -12, 34, 89, -567, -1000, int.MaxValue },
        TypeArgs = new[] { typeof(int) })]
    [Category("SortBy")]
    public class EnumerableSequencesSortByFixture<T>
    {
        private readonly List<T> source;
        private readonly List<T> expected;
        private readonly IComparer<T> comparer;

        public EnumerableSequencesSortByFixture(IEnumerable<T> source, IEnumerable<T> expected)
        {
            this.expected = new List<T>(expected);
            this.source = new List<T>(source);
            this.comparer = ComparerCreator(typeof(T))!;
        }

        [Test]
        [Order(1)]
        public void SortBy_With_Initial_Sequence() =>
            CollectionAssert.AreEqual(this.expected, this.source.SortBy(this.comparer));

        [Test]
        [Order(2)]
        public void SortBy_After_Add_New_Element_To_Source_Sequence_Actual_Result()
        {
            this.expected.Insert(0, default!);

            var actual = this.source.SortBy(this.comparer);
            this.source.Add(default!);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(3)]
        public void SortBy_After_Remove_Element_From_Source_Sequence_Actual_Result()
        {
            this.expected.RemoveAt(0);

            var actual = this.source.SortBy(this.comparer);
            this.source.RemoveAt(this.source.Count - 1);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(0)]
        public void SortBy_Source_Is_Null_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<T>)null!).SortBy(this.comparer), $"Source can not be null.");
        }

        private static IComparer<T>? ComparerCreator(Type type) => type switch
        {
            _ when type == typeof(string) => (IComparer<T>)new StringByLengthComparer(),
            _ when type == typeof(int) => (IComparer<T>)new IntegerByAbsComparer(),
            _ => null
        };
    }
}
