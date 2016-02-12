using System;
using System.IO;

namespace Valentine
{
    public sealed class FileCache : IDisposable
    {
        public string FilePath { get; private set; }

        public FileCache(byte[] mem, string extention = null) : this(new MemoryStream(mem), extention)
        {
        }

        public FileCache(Stream stream, string extention = null)
        {
            FilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + (extention != null ? "." + extention : ".tmp"));
            using (var fs = File.OpenWrite(FilePath))
            {
                byte[] buffer = new byte[32 * 1024];
                int chunk;
                while ((chunk = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, chunk);
                }
            }
            stream.Close();
        }

        public void Dispose()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}
