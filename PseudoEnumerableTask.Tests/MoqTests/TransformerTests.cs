using Moq;
using NUnit.Framework;
using PseudoEnumerableTask.Interfaces;
using Transformers;

namespace PseudoEnumerableTask.Tests.MoqTests
{
    [TestFixture]

    public class TransformerTests
    {
        [TestCase(0.2345E-12, "0011110101010000100000000110000001011111000011101110100001011011")]
        [TestCase(-451387.2345, "1100000100011011100011001110110011110000001000001100010010011100")]
        [TestCase(4294967295.012, "0100000111101111111111111111111111111111111000000110001001001110")]
        [TestCase(double.NegativeInfinity, "1111111111110000000000000000000000000000000000000000000000000000")]
        public void TransformDoubleTest(double value, string expected)
        {
            var mockPredicate = new Mock<ITransformer<double, string>>();

            mockPredicate
                .Setup(p => p.Transform(value))
                .Returns(expected);

            ITransformer<double, string> transformer = mockPredicate.Object;

            Assert.AreEqual(expected, transformer.Transform(value));

            mockPredicate.Verify(p => p.Transform(It.IsAny<double>()), Times.Exactly(1));
        }

        [Test]
        public void TransformTests()
        {
            var source = new[] { 122.625, -255.255, 255.255, 4294967295.012, -451387.2345, 0.2345E-12 };

            var expected = new[]
            {
                "0100000001011110101010000000000000000000000000000000000000000000",
                "1100000001101111111010000010100011110101110000101000111101011100",
                "0100000001101111111010000010100011110101110000101000111101011100",
                "0100000111101111111111111111111111111111111000000110001001001110",
                "1100000100011011100011001110110011110000001000001100010010011100",
                "0011110101010000100000000110000001011111000011101110100001011011",
            };

            var mockPredicate = new Mock<ITransformer<double, string>>();

            mockPredicate
                .Setup(t => t.Transform(It.IsAny<double>()))
                .Returns((double d) => new Ieee754FormatTransformer().Transform(d));

            ITransformer<double, string> transformer = mockPredicate.Object;

            var actual = source.Transform(transformer);

            CollectionAssert.AreEqual(actual, expected);

            mockPredicate.Verify(p => p.Transform(It.IsAny<double>()), Times.Exactly(source.Length));
        }
    }
}
