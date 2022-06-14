using Xunit;

namespace Example.Utils.Tests
{

    public class DateTimeExtensions_ParseOrMinValue_Tests
    {
        private string? _input;
        private DateTime _result;

        [Theory]
        [InlineData("13/12/0001")]
        public void ValidDate(string date)
        {
            GivenAnInput(date);
            WhenParseOrMinValueIsCalled();
            ReturnADate();
        }

        [Theory]
        [InlineData("")]
        [InlineData("13/120001")]
        public void InvalidDate(string date)
        {
            GivenAnInput(date);
            WhenParseOrMinValueIsCalled();
            ReturnMinValue();
        }

        [Fact]
        public void MinValue()
        {
            GivenAnInput(null);
            WhenParseOrMinValueIsCalled();
            ReturnMinValue();
        }

        private void ReturnMinValue()
        {
            Assert.Equal(DateTime.MinValue, _result);
        }

        private void ReturnADate()
        {
            Assert.IsType<DateTime>(_result);
        }

        private void WhenParseOrMinValueIsCalled()
        {
            _result = DateTimeExtensions.ParseOrMinValue(_input);
        }

        private void GivenAnInput(string? date)
        {
            _input = date;
        }
    }
}
