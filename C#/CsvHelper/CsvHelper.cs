using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Example.Helpers
{
    public interface ICsvHelperService
    {
        Task<Stream> CreateCSVStreamAsync<T>(IEnumerable<T> data);
    }
}
