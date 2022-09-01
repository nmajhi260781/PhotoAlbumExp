using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PhotoAlbumExp.Data;
using System;
using System.Linq;
using Xunit;

namespace PhotoAlbumExp.Tests
{
    /// <summary>
    /// Test class to test Data access layer
    /// </summary>
    public class PhotoAlbumDataAccessTests
    {       
        private PhotoAlbumDataAccess _dataAccess { get; set; }
        public PhotoAlbumDataAccessTests()
        {
            _dataAccess = new PhotoAlbumDataAccess(new MockLogger(), new MockConfiguration("http://jsonplaceholder.typicode.com/"));
        }
        
        /// <summary>
        /// Testing all albums are returned
        /// </summary>
        [Fact]
        public void GetAllAlbums_ShouldReturnAlbums()
        {
            //Act
            var albums = _dataAccess.GetAllAlbums();

            //Assert
            Assert.NotNull(albums);
            Assert.True(albums.Count > 0);
        }

        /// <summary>
        /// Testing all photos are returned
        /// </summary>
        [Fact]
        public void GetAllPhotos_ShouldReturnPhotos()
        {

            //Act
            var photos = _dataAccess.GetAllPhotos();

            //Assert
            Assert.NotNull(photos);
            Assert.True(photos.Count > 0);
        }

        /// <summary>
        /// Testing all albums and photos are returned for a positive user id passed
        /// </summary>
        /// <param name="albumUserId"></param>
        /// <param name="expectedAlbumPhotosCount"></param>
        [Theory]
        [InlineData(1, 500)]
        [InlineData(2, 500)]
        [InlineData(3, 500)]
        [InlineData(55555, 0)]
        public void GetAlbumPhotos_PositiveAlbumUserId_ShouldReturnAlbumPhotos(int albumUserId, int expectedAlbumPhotosCount)
        {
            //Act
            var albumPhotos = _dataAccess.GetAlbumPhotos(albumUserId).ToList();

            //Assert
            Assert.NotNull(albumPhotos);
            Assert.Equal(expectedAlbumPhotosCount,albumPhotos.Count());
        }

        /// <summary>
        /// Testing all albums and photos are returned for no user id passed
        /// </summary>
        /// <param name="albumUserId"></param>
        [Theory]
        [InlineData(null)]
        public void GetAlbumPhotos_NullableAlbumUserId_ShouldReturnAllAlbumPhotos(int? albumUserId)
        {
            //Act
            var albumPhotosCount = _dataAccess.GetAlbumPhotos(albumUserId)?.ToList().Count() ?? 0;
            var allPhotosCount = _dataAccess.GetAllPhotos().Count();
            //Assert
            
            Assert.True(albumPhotosCount == allPhotosCount);
        }
    }
}
