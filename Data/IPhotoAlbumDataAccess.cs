using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbumExp.Model;

namespace PhotoAlbumExp.Data
{
    /// <summary>
    /// Interface for Data Access Class
    /// </summary>
    public interface IPhotoAlbumDataAccess
    {
        public IEnumerable<AlbumPhoto> GetAlbumPhotos(int? albumUserId);
    }
}
