using System.Collections.Generic;
using System.IO;

namespace Example
{
    public interface IPdfCreator
    {
        Stream FromValues(string pdfTemplate, IDictionary<string, string> values);
    }
}
