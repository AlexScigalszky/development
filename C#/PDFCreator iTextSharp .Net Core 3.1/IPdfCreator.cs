using System.Collections.Generic;

namespace Example
{
    public interface IPdfCreator
    {
        byte[] FromValues(string pdfTemplate, IDictionary<string, string> values); 
    }
}
