using System;
using System.Collections.Generic;
using Comparers;
using NUnit.Framework;
using Predicates;
using PseudoEnumerableTask.Interfaces;
using Transformers;

#pragma warning disable SA1600
#pragma warning disable CA1707

namespace PseudoEnumerableTask.Tests.NUnitTests
{
    [TestFixture]
    public class EnumerableSequencesTests
    {
        private static IEnumerable<TestCaseData> FilterTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new ContainsDigitPredicate { Digit = 0 },
                    new List<int> { 2212332, 1405644, -1236674 },
                    new List<int> { 1405644 });
                yield return new TestCaseData(
                    new ContainsDigitPredicate() { Digit = 7 },
                    new List<int>()
                    {
                        -27,
                        173,
                        371132,
                        7556,
                        7243,
                        10017,
                        int.MinValue,
                        int.MaxValue
                    },
                    new List<int>
                    {
                        -27,
                        173,
                        371132,
                        7556,
                        7243,
                        10017,
                        int.MinValue,
                        int.MaxValue
                    });
                yield return new TestCaseData(
                    new ContainsDigitPredicate { Digit = 0 },
                    new List<int>
                    {
                        int.MinValue,
                        int.MinValue,
                        int.MinValue,
                        int.MaxValue,
                        int.MaxValue
                    },
                    new List<int> { });
                yield return new TestCaseData(
                    new ContainsDigitPredicate { Digit = 2 },
                    new List<int>
                    {
                        -123,
                        123,
                        2202,
                        3333,
                        4444,
                        55055,
                        0,
                        -7,
                        5402,
                        9,
                        0,
                        -150,
                        287
                    }, new List<int>
                    {
                        -123,
                        123,
                        2202,
                        5402,
                        287
                    });
            }
        }

        private static IEnumerable<TestCaseData> FilterWithDelegateTestCases
        {
            get
            {
                yield return new TestCaseData(0,
                    new List<int> { 2212332, 1405644, -1236674 },
                    new List<int> { 1405644 });
                yield return new TestCaseData(
                    7,
                    new List<int>()
                    {
                        -27,
                        173,
                        371132,
                        7556,
                        7243,
                        10017,
                        int.MinValue,
                        int.MaxValue
                    },
                    new List<int>
                    {
                        -27,
                        173,
                        371132,
                        7556,
                        7243,
                        10017,
                        int.MinValue,
                        int.MaxValue
                    });
                yield return new TestCaseData(
                    0,
                    new List<int>
                    {
                        int.MinValue,
                        int.MinValue,
                        int.MinValue,
                        int.MaxValue,
                        int.MaxValue
                    },
                    new List<int> { });
                yield return new TestCaseData(
                    2,
                    new List<int>
                    {
                        -123,
                        123,
                        2202,
                        3333,
                        4444,
                        55055,
                        0,
                        -7,
                        5402,
                        9,
                        0,
                        -150,
                        287
                    }, new List<int>
                    {
                        -123,
                        123,
                        2202,
                        5402,
                        287
                    });
            }
        }

        private static IEnumerable<TestCaseData> TransformerTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new Ieee754FormatTransformer(),
                    new List<double>
                    {
                        122.625,
                        -255.255,
                        255.255,
                        4294967295.012,
                        -451387.2345,
                        0.2345E-12
                    },
                    new List<string>()
                    {
                        "0100000001011110101010000000000000000000000000000000000000000000",
                        "1100000001101111111010000010100011110101110000101000111101011100",
                        "0100000001101111111010000010100011110101110000101000111101011100",
                        "0100000111101111111111111111111111111111111000000110001001001110",
                        "1100000100011011100011001110110011110000001000001100010010011100",
                        "0011110101010000100000000110000001011111000011101110100001011011"
                    });
                yield return new TestCaseData(new Ieee754FormatTransformer(),
                    new List<double>
                    {
                        double.PositiveInfinity,
                        0.0,
                        double.NegativeInfinity,
                        -0.0,
                        double.Epsilon,
                        double.NaN
                    },
                    new List<string>()
                    {
                        "0111111111110000000000000000000000000000000000000000000000000000",
                        "0000000000000000000000000000000000000000000000000000000000000000",
                        "1111111111110000000000000000000000000000000000000000000000000000",
                        "1000000000000000000000000000000000000000000000000000000000000000",
                        "0000000000000000000000000000000000000000000000000000000000000001",
                        "1111111111111000000000000000000000000000000000000000000000000000"
                    });
            }
        }
        
        private static IEnumerable<TestCaseData> TransformerWithDelegateTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new List<double>
                    {
                        122.625,
                        -255.255,
                        255.255,
                        4294967295.012,
                        -451387.2345,
                        0.2345E-12
                    },
                    new List<string>()
                    {
                        "0100000001011110101010000000000000000000000000000000000000000000",
                        "1100000001101111111010000010100011110101110000101000111101011100",
                        "0100000001101111111010000010100011110101110000101000111101011100",
                        "0100000111101111111111111111111111111111111000000110001001001110",
                        "1100000100011011100011001110110011110000001000001100010010011100",
                        "0011110101010000100000000110000001011111000011101110100001011011"
                    });
                yield return new TestCaseData(
                    new List<double>
                    {
                        double.PositiveInfinity,
                        0.0,
                        double.NegativeInfinity,
                        -0.0,
                        double.Epsilon,
                        double.NaN
                    },
                    new List<string>()
                    {
                        "0111111111110000000000000000000000000000000000000000000000000000",
                        "0000000000000000000000000000000000000000000000000000000000000000",
                        "1111111111110000000000000000000000000000000000000000000000000000",
                        "1000000000000000000000000000000000000000000000000000000000000000",
                        "0000000000000000000000000000000000000000000000000000000000000001",
                        "1111111111111000000000000000000000000000000000000000000000000000"
                    });
            }
        }

        private static IEnumerable<TestCaseData> SortByTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new StringByLengthComparer(),
                    new List<string>
                    {
                        "Beg",
                        null,
                        "Life",
                        "I",
                        "i",
                        "I",
                        null,
                        "To"
                    }, new List<string>
                    {
                        null,
                        null,
                        "I",
                        "i",
                        "I",
                        "To",
                        "Beg",
                        "Life"
                    });
                yield return new TestCaseData(
                    new StringByLengthComparer(),
                    new List<string>
                    {
                        null,
                        "Longer",
                        "Longest",
                        "Short",
                        null,
                        null
                    }, new List<string>
                    {
                        null,
                        null,
                        null,
                        "Short",
                        "Longer",
                        "Longest"
                    });
            }
        }
        
        private static IEnumerable<TestCaseData> SortByWithDelegateTestCases
        {
            get
            {
                yield return new TestCaseData(
                    new List<string>
                    {
                        "Beg",
                        null,
                        "Life",
                        "I",
                        "i",
                        "I",
                        null,
                        "To"
                    }, new List<string>
                    {
                        null,
                        null,
                        "I",
                        "i",
                        "I",
                        "To",
                        "Beg",
                        "Life"
                    });
                yield return new TestCaseData(
                    new List<string>
                    {
                        null,
                        "Longer",
                        "Longest",
                        "Short",
                        null,
                        null
                    }, new List<string>
                    {
                        null,
                        null,
                        null,
                        "Short",
                        "Longer",
                        "Longest"
                    });
            }
        }

        [TestCaseSource(nameof(TransformerTestCases))]
        public void TransformerTests(ITransformer<double, string> transformer, IEnumerable<double> source,
            IEnumerable<string> excepted)
        {
            CollectionAssert.AreEqual(excepted, source.Transform(transformer));
        }

        [TestCaseSource(nameof(TransformerWithDelegateTestCases))]
        public void TransformerWithDelegateTests(IEnumerable<double> source,
            IEnumerable<string> excepted)
        {
            Converter<double, string> converter = new Ieee754FormatTransformer().Transform;
            CollectionAssert.AreEqual(excepted, source.Transform(converter));
        }

        [TestCaseSource(nameof(SortByTestCases))]
        public void SortByTests(IComparer<string> comparer, IEnumerable<string> source, IEnumerable<string> excepted)
        {
            CollectionAssert.AreEqual(excepted, source.SortBy(comparer));
        }

        [TestCaseSource(nameof(SortByWithDelegateTestCases))]
        public void SortByWithDelegateTests(IEnumerable<string> source, IEnumerable<string> excepted)
        {
            Comparison<string> comparer = new StringByLengthComparer().Compare;
            CollectionAssert.AreEqual(excepted, source.SortBy(comparer));
        }

        [TestCaseSource(nameof(FilterTestCases))]
        public void FilterByTests(IPredicate<int> predicate, IEnumerable<int> source, IEnumerable<int> excepted)
        {
            CollectionAssert.AreEqual(excepted, source.Filter(predicate));
        }

        [TestCaseSource(nameof(FilterWithDelegateTestCases))]
        public void FilterByWithDelegateTests(int digit, IEnumerable<int> source, IEnumerable<int> excepted)
        {
            CollectionAssert.AreEqual(excepted, source.Filter(x => x.ToString().Contains(digit.ToString())));
        }

        [TestCaseSource(nameof(TransformerTestCases))]
        public void Transformer_WithActualElements_Tests(ITransformer<double, string> transformer, List<double> source,
            List<string> excepted)
        {
            source.RemoveAt(2);
            var actual = source.Transform(transformer);
            excepted.RemoveAt(2);
            CollectionAssert.AreEqual(excepted, actual);
        }
    }
}
