using FluentAssertions;
using PracticeUnitTest.Fundamentals;
using Xunit;

namespace PracticeUnitTest.Tests.Fundamentals
{
    public class FizzBuzzTests
    {
        [Fact]
        public void GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz()
        {
            // Act
            var actual = FizzBuzz.GetOutput(15);

            // Assert
            actual.Should().Be("FizzBuzz");
        }

        [Fact]
        public void GetOutput_InputIsDivisibleBy3Only_ReturnFizz()
        {
            // Act
            var actual = FizzBuzz.GetOutput(3);

            // Assert
            actual.Should().Be("Fizz");
        }

        [Fact]
        public void GetOutput_InputIsDivisibleBy5Only_ReturnBuzz()
        {
            // Act
            var actual = FizzBuzz.GetOutput(5);

            // Assert
            actual.Should().Be("Buzz");
        }

        [Fact]
        public void GetOutput_InputIsNotDivisibleBy3Or5_ReturnTheSameNumber()
        {
            // Act
            var actual = FizzBuzz.GetOutput(1);

            // Assert
            actual.Should().Be("1");
        }
    }
}