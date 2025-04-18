using System.IO;

namespace Proxy
{
    public class RealAlbum : IAlbum
    {
        public FileStream GetAlbum(string filename)
            => File.Open(filename, FileMode.Open);
    }
}
