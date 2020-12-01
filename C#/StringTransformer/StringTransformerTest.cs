using Example.Utils;
using Xunit;

namespace Example.Tests.Utils
{
    public class StringTransformerTest
    {

        [Theory]
        [InlineData("aàáâãäåçc", "aaaaaaacc", true)]
        [InlineData("Alex", "alex", false)]
        [InlineData("Nicolás", "nicolás", false)]
        [InlineData("Nicolás", "nicolas", false)]
        [InlineData("€", "€", true)]
        [InlineData("~", "~", true)]
        [InlineData("Je veux aller à Saint-Étienne", "Je veux aller a Saint-Etienne", true)]
        [InlineData("Cà phê sữa đá hay còn gọi đơn giản là cà phê sữa là một loại thức uống thông dụng ở Việt Nam.",
                    "Ca phe sua đa hay con goi đon gian la ca phe sua la mot loai thuc uong thong dung o Viet Nam.", true)]
        public void Match_With_Equal_Operator(string src, string dest, bool expected)
        {
            var result = src.RemoveDiacritics() == dest;
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("aàáâãäåçc", "aaaaaaacc", true)]
        [InlineData("Alex", "alex", false)]
        [InlineData("Nicolás", "nicolás", false)]
        [InlineData("Nicolás", "nicolas", false)]
        [InlineData("€", "€", true)]
        [InlineData("~", "~", true)]
        [InlineData("Je veux aller à Saint-Étienne", "Je veux aller a Saint-Etienne", true)]
        [InlineData("Cà phê sữa đá hay còn gọi đơn giản là cà phê sữa là một loại thức uống thông dụng ở Việt Nam.",
                    "Ca phe sua đa hay con goi đon gian la ca phe sua la mot loai thuc uong thong dung o Viet Nam.", true)]
        public void Match_With_Equals_Method(string src, string dest, bool expected)
        {
            var result = src.RemoveDiacritics().Equals(dest);
            Assert.Equal(expected, result);
        }

    }
}