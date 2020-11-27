using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Converters.Base64
{
    public class Base64Converter : IBase64Converter
    {
        public byte[] FromBase64String(string s)
        {
            return Convert.FromBase64String(s);
        }
    }
}