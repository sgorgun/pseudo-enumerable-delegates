using Moq;
using NUnit.Framework;
using Predicates;
using PseudoEnumerableTask.Interfaces;

namespace PseudoEnumerableTask.Tests.MoqTests
{
    [TestFixture]
    public class PredicateTests
    {
        [TestCase(55)]
        [TestCase(551)]
        [TestCase(-12551)]
        [TestCase(-90551)]
        public void VerifyTests_Return_True(int value)
        {
            var mockPredicate = new Mock<IPredicate<int>>();

            mockPredicate
                .Setup(p => p.Verify(It.Is<int>(i => new ContainsDigitPredicate() { Digit = 5 }
                .Verify(i))))
                .Returns(true);

            IPredicate<int> predicate = mockPredicate.Object;

            Assert.IsTrue(predicate.Verify(value));

            mockPredicate.Verify(p => p.Verify(It.IsAny<int>()), Times.Exactly(1));
        }

        [TestCase(109)]
        [TestCase(67632)]
        [TestCase(-120943)]
        [TestCase(-2113)]
        public void VerifyTests_Return_False(int value)
        {
            Mock<IPredicate<int>> mockPredicate = new Mock<IPredicate<int>>();

            mockPredicate
                .Setup(p => p.Verify(It.Is<int>(i =>
                    new ContainsDigitPredicate() { Digit = 5 }.Verify(i))))
                .Returns(true);

            IPredicate<int> predicate = mockPredicate.Object;

            Assert.IsFalse(predicate.Verify(value));

            mockPredicate.Verify(p => p.Verify(It.IsAny<int>()), Times.Exactly(1));
        }

        [Test]
        public void FilterTests()
        {
            var source = new[] { 12, 35, -65, 543, 23 };

            var expected = new[] { 35, -65, 543 };

            Mock<IPredicate<int>> mockPredicate = new Mock<IPredicate<int>>();

            mockPredicate
                .Setup(p => p.Verify(It.Is<int>(i =>
                    new ContainsDigitPredicate { Digit = 5 }.Verify(i))))
                .Returns(true);

            IPredicate<int> predicate = mockPredicate.Object;

            var actual = source.Filter(predicate);

            CollectionAssert.AreEqual(actual, expected);

            mockPredicate.Verify(p => p.Verify(It.IsAny<int>()), Times.Exactly(source.Length));
        }
    }
}
