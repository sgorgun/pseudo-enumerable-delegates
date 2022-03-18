using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoEnumerableTask.Interfaces;
using Transformers;

namespace PseudoEnumerableTask.Tests.NUnitTests
{
    [TestFixture(
        new[] { 122.625, -255.255, 255.255, 4294967295.012, -451387.2345 },
        new[]
        {
            "0100000001011110101010000000000000000000000000000000000000000000",
            "1100000001101111111010000010100011110101110000101000111101011100",
            "0100000001101111111010000010100011110101110000101000111101011100",
            "0100000111101111111111111111111111111111111000000110001001001110",
            "1100000100011011100011001110110011110000001000001100010010011100",
        },
        0.2345E-12,
        "0011110101010000100000000110000001011111000011101110100001011011",
        TypeArgs = new Type[] { typeof(double), typeof(string) })]
    [TestFixture(
        new[] { "One", "two", "three", "four", "five", "six", "seven" },
        new[] { 3, 3, 5, 4, 4, 3, 5 },
        "hello world",
        11,
        TypeArgs = new Type[] { typeof(string), typeof(int) })]
    [Category("Transform")]
    public class EnumerableSequencesTransformFixture<TSource, TResult>
    {
        private readonly List<TSource> source;
        private readonly List<TResult> expected;
        private readonly TResult inResult;
        private readonly TSource inSource;
        private readonly ITransformer<TSource, TResult> transformer;

        public EnumerableSequencesTransformFixture(IEnumerable<TSource> source, IEnumerable<TResult> expected, TSource inSource, TResult inResult)
        {
            this.expected = new List<TResult>(expected);
            this.source = new List<TSource>(source);
            this.inResult = inResult;
            this.inSource = inSource;
            this.transformer = TransformerCreator(typeof(TSource), typeof(TResult))!;
        }

        [Test]
        [Order(1)]
        public void Transform_With_Initial_Sequence()
        {
            IEnumerable<TSource> enumerable = this.source;
            var actual = enumerable.Transform(this.transformer);
            CollectionAssert.AreEqual(actual, this.expected);
        }

        [Test]
        [Order(2)]
        public void Transform_After_Add_New_Element_To_Source_Sequence_Actual_Result()
        {
            this.expected.Add(this.inResult);

            var actual = this.source.Transform(this.transformer);
            this.source.Add(this.inSource);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(3)]
        public void Transform_After_Remove_Element_From_Source_Sequence_Actual_Result()
        {
            this.expected.Remove(this.inResult);

            var actual = this.source.Transform(this.transformer);
            this.source.Remove(this.inSource);

            CollectionAssert.AreEqual(this.expected, actual);
        }

        [Test]
        [Order(0)]
        public void Transform_Source_Is_Null_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ((IEnumerable<TSource>)null!).TypeOf<TSource>(), $"Source can not be null.");
        }

        private static ITransformer<TSource, TResult>? TransformerCreator(Type typeSource, Type typeResult)
            => (typeSource, typeResult) switch
            {
                _ when (typeSource, typeResult) == (typeof(double), typeof(string)) =>
                    (ITransformer<TSource, TResult>)new Ieee754FormatTransformer(),
                _ when (typeSource, typeResult) == (typeof(string), typeof(int)) =>
                    (ITransformer<TSource, TResult>)new StringLengthTransformer(),
                _ => null
            };
    }
}
