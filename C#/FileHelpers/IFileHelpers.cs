using System.Collections.Generic;

namespace CBV_SB_Shared.FileHelpers
{
    public interface IFileHelpers
    {
        public void GetDirectories();

        public List<string> GetFiles(string pathToFile);

        public bool CreateDirectory(string path);

        public List<string> ReadFile(string filePath);

        public string ReadLineFromFile(string filePath, int line);

        public void WriteFile(string path, IEnumerable<string> content);

        public void ReadFileToMemory(string filePath);

        public string ReadLineFromMemory(int line);

        public void ClearMemory();

        void CopyAndDelete(string source, string destination);
    }
}
