using System.Collections.Generic;
using System.IO;

namespace Proxy
{
    public class LazyAlbum : IAlbum
    {
        private readonly RealAlbum _realAlbum = new();
        private readonly Dictionary<string, FileStream> _albums = [];

        public FileStream GetAlbum(string filename)
        {
            if (_albums.TryGetValue(filename, out FileStream? stream))
            {
                return stream;
            }
            FileStream album = _realAlbum.GetAlbum(filename);
            _albums.Add(filename, album);
            return album;
        }
    }
}
