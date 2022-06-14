using Xunit;

namespace Example.Utils.Tests
{
    public class StringExtension_Is1900_Tests
    {
        private string _input = string.Empty;
        private bool _result;

        [Theory]
        [InlineData("00/01/1900", true)]
        public void IsValidDateTest(string value, bool result)
        {
            GivenAValidInput(value);
            WhenIs1900IsCalled();
            ThenReturn(result);
        }

        private void ThenReturn(bool v)
        {
            Assert.Equal(v, _result);
        }

        private void WhenIs1900IsCalled()
        {
            _result = _input.Is1900();
        }

        private void GivenAValidInput(string value)
        {
            _input = value;
        }
    }
}
