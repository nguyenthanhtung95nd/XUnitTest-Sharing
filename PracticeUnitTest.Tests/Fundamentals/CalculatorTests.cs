using FluentAssertions;
using PracticeUnitTest.Fundamentals;
using Xunit;

namespace PracticeUnitTest.Tests.Fundamentals
{
    public class CalculatorTests
    {
        [Fact]
        public void Adding_Two_Numbers_1()
        {
            // Arrange
            var value1 = 1;
            var value2 = 3;
            // The leakage
            // Leaking algorithm implementation
            var expected = value1 + value2;

            // Act
            var actual = Calculator.Add(value1, value2);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(11, 33)]
        [InlineData(100, 500)]
        public void Adding_Two_Numbers_2(int value1, int value2) 
        {
            // Arrange
            // The leakage
            // A parameterized version of the same test
            var expected = value1 + value2; 

            // Act
            var actual = Calculator.Add(value1, value2);

            // Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 3, 4)]
        [InlineData(11, 33, 44)]
        [InlineData(100, 500, 600)]
        public void Adding_Two_Numbers(int value1, int value2, int expected)
        {
            // Act
            var actual = Calculator.Add(value1, value2);

            // Assert
            actual.Should().Be(expected);
        }
    }
}