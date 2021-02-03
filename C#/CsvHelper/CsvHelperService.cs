using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using System.Text;
using System.Globalization;

namespace Example.Helpers
{
    public class CsvHelperService : ICsvHelperService
    {
        private const string ERROR_CREATE_STREAM = "Error creating CSV. ";
        private const string DATE_FORMAT = "dd/MMM/yyyy";

        public async Task<Stream> CreateCSVStreamAsync<T>(IEnumerable<T> data)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                
                using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[] { DATE_FORMAT };
                    await csv.WriteRecordsAsync<T>(data);
                }
                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception(ERROR_CREATE_STREAM + ex.Message);
            }
        }
    }
}
