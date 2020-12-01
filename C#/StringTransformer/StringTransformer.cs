using System;
using System.Globalization;
using System.Text;

namespace Example.Utils
{
    public static class StringTransformer
    {
        /// <summary>
        /// Remove Diacritics from a string
        /// This converts accented characters to nonaccented, which means it is
        /// easier to search for matching data with or without such accents.
        /// This code from Micheal Kaplans Blog:
        ///    http://blogs.msdn.com/b/michkap/archive/2007/05/14/2629747.aspx
        /// Respaced and converted to an Extension Method
        /// <example>
        ///    aàáâãäåçc
        /// is converted to
        ///    aaaaaaacc
        /// </example>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
