using Example.Models;
using System;
using System.Collections.Generic;

namespace Example.Services.Export
{
    public interface IExportStrategy
    {
        string GetExtension();
        string Export(IEnumerable<IExportable> templaes);
        string Export(IExportable tempalte, IEnumerable<IInstanceExportable> instanaces);
    }
}
