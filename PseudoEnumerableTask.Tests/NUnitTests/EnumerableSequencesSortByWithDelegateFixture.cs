using System;
using System.Collections.Generic;
using NUnit.Framework;

#pragma warning disable SA1600
#pragma warning disable CA1707

namespace PseudoEnumerableTask.Tests.NUnitTests
{
    [TestFixture]
    public class EnumerableSequencesMethodsWithDelegateTest
    {
        private static IEnumerable<TestCaseData> FilterTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new [] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" },
                    new [] { "one", "two", "six", "ten" },
                    new Predicate<string>(x => x.Length == 3));
                yield return new TestCaseData(
                    new [] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" },
                    new [] { "one", "two", "four"},
                    new Predicate<string>(x => x.Contains('o')));
                yield return new TestCaseData(
                    new [] { "one", "two", "Two", "Three", "three", "four", "five", "six", "seven", "eight", "nine", "ten" },
                    new [] {  "two", "Two", "Three", "three", "ten" },
                    new Predicate<string>(x => x.ToUpper().StartsWith('T')));
            }
        }

        [TestCase(
            new[] { -9.56, 67.908, 45.34, 0.123, -100.453 },
            new[] { 0.123, -9.56, 45.34, 67.908, -100.453 })]
        public void SortByTests(double[] source, double[] expected) =>
            Assert.AreEqual(expected, source.SortBy((x, y) => Math.Abs(x).CompareTo(Math.Abs(y))));

        [TestCase(
            new[] { "one", "two", "three", "four", null, "five", "six", "seven", "eight", null, "nine", "ten" },
            new[] { null, null, "one", "two", "six", "ten", "four", "five", "nine", "three", "seven", "eight" })]
        public void SortByTests(string[] source, string[] expected) =>
            Assert.AreEqual(expected, source.SortBy((x, y) => (x?.Length ?? 0).CompareTo(y?.Length ?? 0)));

        [TestCase(
            new[] { 12, 56, -907, 567, 234, -576, -43253, 1234 },
            new[] { 12, 56, 567, 234, 1234 })]
        public void FilterTests(int[] source, int[] expected) =>
            Assert.AreEqual(expected, source.Filter(x => x > 0));
        
        [TestCaseSource(nameof(FilterTestCases))]
        public void FilterTests(string[] source, string[] expected, Predicate<string> predicate) =>
            CollectionAssert.AreEqual(expected, source.Filter(predicate));
    }
}
