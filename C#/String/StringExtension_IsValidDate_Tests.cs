using Xunit;

namespace Example.Utils.Tests
{
    public class StringExtension_IsValidDate_Tests
    {
        private string _input = string.Empty;
        private bool _result;

        [Theory]
        [InlineData("11/22/99", true)]
        [InlineData("", false)]
        public void IsValidDateTest(string value, bool result)
        {
            GivenAValidInput(value);
            WhenIsValidIsCalled();
            ThenReturn(result);
        }

        private void ThenReturn(bool v)
        {
            Assert.Equal(v, _result);
        }

        private void WhenIsValidIsCalled()
        {
            _result = _input.IsValidDate();
        }

        private void GivenAValidInput(string value)
        {
            _input = value;
        }
    }
}
