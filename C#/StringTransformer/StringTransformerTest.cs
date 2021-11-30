using Example.Utils;
using Xunit;

namespace Example.Tests.Utils
{
    public static class TestDataShare
    {
        public static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[] { "aàáâãäåçc", "aaaaaaacc", true },
                yield return new object[] { "Alex", "alex", false },
                yield return new object[] { "Nicolás", "nicolás", false },
                yield return new object[] { "Nicolás", "nicolas", false },
                yield return new object[] { "€", "€", true },
                yield return new object[] { "~", "~", true },
                yield return new object[] { "Je veux aller à Saint-Étienne", "Je veux aller a Saint-Etienne", true },
                yield return new object[]{ "Cà phê sữa đá hay còn gọi đơn giản là cà phê sữa là một loại thức uống thông dụng ở Việt Nam.",
                            "Ca phe sua đa hay con goi đon gian la ca phe sua la mot loai thuc uong thong dung o Viet Nam.", true },
            }
        }
    }

    public class StringTransformerTest
    {

        [Theory]
        [MemberData(nameof(TestDataShare.Data, MemberType = typeof(TestDataShare)))]
        public void Match_With_Equal_Operator(string src, string dest, bool expected)
        {
            var result = src.RemoveDiacritics() == dest;
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(TestDataShare.Data, MemberType = typeof(TestDataShare)))]
        public void Match_With_Equals_Method(string src, string dest, bool expected)
        {
            var result = src.RemoveDiacritics().Equals(dest);
            Assert.Equal(expected, result);
        }

    }
}