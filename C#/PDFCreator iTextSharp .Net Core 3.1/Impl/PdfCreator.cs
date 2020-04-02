using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace Example.Impl
{
    public class PdfCreator : IPdfCreator
    {
        public Stream FromValues(string pdfTemplate, IDictionary<string, string> values)
        {
            var pdfStream = new FileStream(path: pdfTemplate, mode: FileMode.Open);
            var outStream = new MemoryStream();

            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;

            try
            {
                pdfReader = new PdfReader(pdfStream);
                pdfStamper = new PdfStamper(pdfReader, outStream);
                AcroFields form = pdfStamper.AcroFields;

                foreach (string f in values.Keys)
                {
                    form.SetField(f, values[f]);
                }

                // set this if you want the result PDF to not be editable. 
                pdfStamper.FormFlattening = true;
                return outStream;
            }
            finally
            {
                pdfStamper?.Close();
                pdfReader?.Close();
            }
        }
    }
}
