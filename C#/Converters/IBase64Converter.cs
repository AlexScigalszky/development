using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Converters
{
    public interface IBase64Converter
    {
        byte[] FromBase64String(string s);
    }
}
