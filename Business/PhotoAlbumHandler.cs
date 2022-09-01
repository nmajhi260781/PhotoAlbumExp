using PhotoAlbumExp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbumExp.Data;
using Microsoft.Extensions.Logging;

namespace PhotoAlbumExp.Business
{
    /// <summary>
    /// Business Level Handler Class
    /// </summary>
    public class PhotoAlbumHandler : IPhotoAlbum
    {
        private readonly IPhotoAlbumDataAccess _photoAlbumDataAccess;
        private readonly ILogger<PhotoAlbumHandler> _logger;

        /// <summary>
        /// Injecting Logger and Data Access class instance 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="custDataAccess"></param>
        public PhotoAlbumHandler(ILogger<PhotoAlbumHandler> logger, IPhotoAlbumDataAccess custDataAccess)
        {
            _logger = logger;
            _photoAlbumDataAccess = custDataAccess;
        }

        /// <summary>
        /// Business Level Handler Function
        /// </summary>
        /// <param name="albumUserId">Album User Id - int?</param>
        /// <returns>IEnumerable of type AlbumPhotoModel</returns>
        public IEnumerable<AlbumPhoto> GetAlbumPhotos(int albumUserId)
        {
            _logger.LogInformation("Calling Data Access Layer to get Album's Photo List " + ((albumUserId != -99) ? ("for User Id " + albumUserId) : "for All User Id"));

            var lstAlbumPhotos = (albumUserId != -99) ? _photoAlbumDataAccess.GetAlbumPhotos(albumUserId) : _photoAlbumDataAccess.GetAlbumPhotos(null);

            return lstAlbumPhotos;            
        }
    }
}
