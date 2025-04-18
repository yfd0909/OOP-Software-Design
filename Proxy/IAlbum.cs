using System.IO;

namespace Proxy
{
    public interface IAlbum
    {
        public FileStream GetAlbum(string filename);
    }
}
