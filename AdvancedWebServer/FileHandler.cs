using System;
using System.IO;
namespace AdvancedWebServer
{
    public class FileHandler
    {
        string filename;
        string path;
        public FileHandler(string Path,string Filename)
        {
            path = Path;
            filename = Filename;
        }
        public byte[] ConvertFileDataBytes()
        {
            try
            {
                FileStream fs = new FileStream(path+filename, FileMode.Open, FileAccess.Read);
                byte[] bytes = System.IO.File.ReadAllBytes(path+filename);

                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                return bytes;
            }
            catch (Exception e)
            {

                Console.WriteLine("FileNotFound");

                FileStream fs = new FileStream("NotFound.html", FileMode.Open, FileAccess.Read);
                byte[] bytes = System.IO.File.ReadAllBytes("NotFound.html");

                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                return bytes;
            }
        }
    }
}
