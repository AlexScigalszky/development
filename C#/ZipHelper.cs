using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.IO;

namespace Example
{
    public class ZipHelper
    {
        /// <summary>
        /// Create a zip with a collections of files inside it
        /// </summary>
        /// <param name="files"></param>
        /// <param name="zipName"></param>
        /// <returns></returns>
        public byte[] buildZip(List<String> files, String zipName)
        {
            MemoryStream buff = new MemoryStream();
            ZipFile file = new ZipFile(zipName);
            foreach (String path in files)
            {
                if (path != "")
                {
                    file.AddFile(path,"");
                }
            }
            file.Save(buff);
            return buff.ToArray();

        }
    }
}
