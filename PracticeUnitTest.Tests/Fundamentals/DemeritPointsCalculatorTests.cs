using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using PracticeUnitTest.Fundamentals;
using Xunit;

namespace PracticeUnitTest.Tests.Fundamentals
{
    public class DemeritPointsCalculatorTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(301)]
        public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowArgumentOutOfRangeException(int speed)
        {
            // Act
            var calculator = new DemeritPointsCalculator();

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculateDemeritPoints(speed));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(64, 0)]
        [InlineData(65, 0)]
        [InlineData(66, 0)]
        [InlineData(70, 1)]
        [InlineData(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int speed, int expectedResult)
        {
            // Arrange
            var calculator = new DemeritPointsCalculator();

            // Act
            var actual = calculator.CalculateDemeritPoints(speed);

            // Assert
            actual.Should().Be(expectedResult);
        }
    }
}
