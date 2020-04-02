using iText.Forms;
using iText.Kernel.Pdf;
using System.Collections.Generic;
using System.IO;

namespace Example.Impl
{
    public class PdfCreator: IPdfCreator
    {
        public byte[] FromValues(string pdfTemplate, IDictionary<string, string> values) 
        {
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            MemoryStream newFile = new MemoryStream();
            PdfWriter writer = new PdfWriter(newFile);

            using (PdfDocument ps = new PdfDocument(pdfReader, writer))
            {
                PdfAcroForm pdfAcroForm  = PdfAcroForm.GetAcroForm(ps, false);
                
                foreach (string f in values.Keys)
                {
                    pdfAcroForm.GetField(f).SetValue(values[f]);
                }

                pdfAcroForm.FlattenFields();
                ps.Close();
            }

            return newFile.ToArray();
        }
    }
}
