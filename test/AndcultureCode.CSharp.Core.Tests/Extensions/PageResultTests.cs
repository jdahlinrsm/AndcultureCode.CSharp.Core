using System.Linq;
using AndcultureCode.CSharp.Core.Models.Errors;
using Xunit;

namespace AndcultureCode.CSharp.Core.Tests.Extensions
{
    public class PageResultTests
    {
        [Fact]
        public void ShouldReturnSuccess()
        {
            //Arrange
            var expectedAnswer = 5;
            var sut = new PagedResult<int>(expectedAnswer, 1);
            //Act
            var answer = sut.Match(x => x, y => 0);
            //Assert
            Assert.Equal(expectedAnswer, answer);
        }

        [Fact]
        public void ShouldReturnFailure()
        {
            //Arrange
            var expectedErrorMessage = "Even in the future nothing works!";
            var sut = new PagedResult<string>(expectedErrorMessage, 1);
            //Act
            var answer = sut.Match(x => x.ToString(), y => y.FirstOrDefault().Message);
            //Assert
            Assert.Equal(expectedErrorMessage, answer);
        }

        [Fact]
        public void ShouldSucceedFirst()
        {
            //Arrange/Act
            var result = IsBelow10(6).Then(IsEven);
            //Assert
            Assert.Equal(6, result.Match(l => l, r => 0));
        }

        [Fact]
        public void ShouldSucceedMultipleCompositions ()
        {
            //Arrange/Act
            var result = IsBelow10(6)
                            .Then(IsAbove0)
                            .Then(IsEven);
            //Assert
            Assert.Equal(6, result.Match(l => l, r => 0));
        }

        [Fact]
        public void ShouldFailFirst()
        {
            //Arrange/Act
            var result = IsBelow10(12).Then(IsEven);
            //Assert
            Assert.Equal(ErrorMessages.NotBelow10, result.Match(l => l.ToString(), r => r.FirstOrDefault().Message));
        }

        [Fact]
        public void ShouldFailSecond()
        {
            //Arrange/Act
            var result = IsBelow10(5).Then(IsEven);
            Assert.Equal(ErrorMessages.NotEven, result.Match(l => l.ToString(), r => r.FirstOrDefault().Message));
        }

        [Fact]
        public void ShouldShortCircuitOnFirstOfTwoFailedValidations()
        {
            //Arrange/Act
            var result = IsBelow10(11).Then(IsEven);
            //Assert
            Assert.Equal(ErrorMessages.NotBelow10, result.Match(l => l.ToString(), r => r.FirstOrDefault().Message));
        }


        private static Result<int> IsBelow10(int number)
        {
            if (number < 10)
                return new Result<int>(number);

            return new Result<int>(ErrorMessages.NotBelow10);
        }

        private static Result<int> IsEven(int number)
        {
            if (number % 2 == 0)
                return new Result<int>(number);

            return new Result<int>(ErrorMessages.NotEven);
        }

        private static Result<int> IsAbove0(int number)
        {
            if (number > 0)
                return new Result<int>(number);

            return new Result<int>(ErrorMessages.NotAbove0);
        }

        internal class ErrorMessages
        {
            public const string None = "";
            public const string NotBelow10 = "Not Below 10!";
            public const string NotEven = "Not Even!";
            public const string NotAbove0 = "Not Above 0!";
        }
    }
}
