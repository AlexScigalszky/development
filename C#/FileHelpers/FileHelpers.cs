using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Example.FileHelpers
{
    public class FileHelpers: IFileHelpers
    {
        public string[] fileContent { get; private set; }

        //metodo de escritura de directorios
        public bool CreateDirectory(string path)
        {
            bool directoryExists = Directory.Exists(path);
            bool finalResult = false;

            if (directoryExists) 
            {
                Console.WriteLine("El directorio indicado ya existe.");
            }
            else
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Se crea el directorio indicado");
                finalResult = true;
            }

            return finalResult;
        }

        //metodo de lectura de directorios
        public void GetDirectories()
        {
            string rootPath = @"C:\Research\demo\FileSystem";

            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);

            foreach (var dir in dirs)
            {
                Console.WriteLine(dir);
            }
        }

        //metodo de listado de archivos
        public List<string> GetFiles(string rootPath)
        {
            List<string> files = new();

            if (Directory.Exists(rootPath))
            {
                var filesWithPath = Directory.GetFiles(rootPath, "*.*", SearchOption.TopDirectoryOnly);

                foreach (var singleFile in filesWithPath)
                {
                    files.Add(Path.GetFileName(singleFile));
                };
            }

            return files;
        }

        //metodo de lectura de archivos
        public List<string> ReadFile(string filePath)
        {
            List<string> lines = new();

            if (File.Exists(filePath))
            {
                lines = File.ReadAllLines(filePath).ToList();
            }
            
            return lines;
        }

        //metodo de escritura de archivos
        public void WriteFile(string filePath, IEnumerable<string> content)
        {
            File.WriteAllLines(filePath, content);
        }

        public string ReadLineFromFile(string filePath, int line)
        {
            var result = "";
            try
            {
                result = File.ReadLines(filePath).Skip(line).Take(1).First();
            }
            catch (Exception)
            {
                //Console.WriteLine($"Exc: { e.Message }");
                result = "";
            }
            return result;
        }

        public string ReadLineFromMemory(int line)
        {
            var result = "";
            try
            {
                result = fileContent[line];
            }
            catch (Exception)
            {
                //Console.WriteLine($"Exc: { e.Message }");
                result = "";
            }
            return result;
        }
        
        public void ReadFileToMemory(string filePath)
        {
            
            fileContent = File.ReadAllLines(filePath);

            //foreach (var line in lines)
            //{
            //    Console.WriteLine(line);
            //}
        }
        
        public void ClearMemory()
        {
            fileContent = null;
        }

        /// <summary>
        /// Copia un archivo desde source a destination y después elimina el archivo en source
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.PathTooLongException"></exception>
        /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        public void CopyAndDelete(string source, string destination)
        {
            //copiamos el archivo procesado y le cambiamos el nombre
            System.IO.File.Copy(source, destination);
            //eliminamos el original
            System.IO.File.Delete(source);
        }
    }
}
