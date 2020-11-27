using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Converters.Base64
{
    public class Base64ConverterMock : IBase64Converter
    {
        public byte[] FromBase64String(string s)
        {
            return new byte[] { };
        }
    }
}