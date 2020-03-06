using ElmahCore;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace EDAviation.Shared.Utils
{
    public class ElmahUtils
    {
        public static string GetJsonFromObject<T>(IQueryable<T> Entities)
        {
            return JsonConvert.SerializeObject(Entities.ToArray());
        }

        public static void ManageExceptionContext(Exception ex)
        {
            ElmahExtensions.RiseError(ex);
        }

        public static MemoryStream GetMemoryStreamFromFile(string WebRootFolder, string FileName)
        {
            var memory = new MemoryStream();
            using (var stream = new FileStream(Path.Combine(WebRootFolder, FileName), FileMode.Open))
                stream.CopyTo(memory);

            memory.Position = 0;
            return memory;
        }
    }
}
