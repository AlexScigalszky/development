using Xunit;

namespace Example.Utils.Tests
{

    public class DateTimeExtensions_ParseOrNull_Tests
    {
        private string? _input;
        private DateTime? _result;

        [Theory]
        [InlineData("13/12/0001")]
        public void ValidDate(string date)
        {
            GivenAnInput(date);
            WhenParseOrNull();
            ReturnADate();
        }

        [Theory]
        [InlineData("")]
        [InlineData("13/120001")]
        public void InvalidDate(string date)
        {
            GivenAnInput(date);
            WhenParseOrNull();
            ReturnNull();
        }

        [Fact]
        public void Null()
        {
            GivenAnInput(null);
            WhenParseOrNull();
            ReturnNull();
        }

        private void ReturnNull()
        {
            Assert.Null(_result);
        }

        private void ReturnADate()
        {
            Assert.IsType<DateTime>(_result);
        }

        private void WhenParseOrNull()
        {
            _result = DateTimeExtensions.ParseOrNull(_input!);
        }

        private void GivenAnInput(string? date)
        {
            _input = date;
        }
    }
}
