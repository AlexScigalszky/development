using Xunit;

namespace Example.Utils.Tests
{
    public class StringExtension_IsEmptyDate_Tests
    {
        private string _input = string.Empty;
        private bool _result;

        [Theory]
        [InlineData("  /  /")]
        [InlineData("  /  /  ")]
        public void IsEmptyDateTest(string value)
        {
            GivenAValidInput(value);
            WhenIsEmptyDateIsCalled();
            ThenReturn(true);
        }

        private void ThenReturn(bool v)
        {
            Assert.Equal(v, _result);
        }

        private void WhenIsEmptyDateIsCalled()
        {
            _result = _input.IsEmptyDate();
        }

        private void GivenAValidInput(string value)
        {
            _input = value;
        }
    }
}
