using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbumExp.Model;

namespace PhotoAlbumExp.Business
{
    /// <summary>
    /// Interface for Business Level Handler Class
    /// </summary>
    public interface IPhotoAlbum
    {
        public IEnumerable<AlbumPhoto> GetAlbumPhotos(int albumUserId);
    }
}
