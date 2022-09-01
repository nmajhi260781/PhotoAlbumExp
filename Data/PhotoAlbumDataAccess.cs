using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PhotoAlbumExp.Model;

namespace PhotoAlbumExp.Data
{
    /// <summary>
    /// Data Access Class
    /// </summary>
    public class PhotoAlbumDataAccess : IPhotoAlbumDataAccess
    {
        private readonly ILogger<PhotoAlbumDataAccess> _logger;
        private string _apiUrl { get; set; }

        /// <summary>
        /// Injecting Logger instance 
        /// </summary>
        /// <param name="logger"></param>
        public PhotoAlbumDataAccess(ILogger<PhotoAlbumDataAccess> logger,IConfiguration config)
        {
            _logger = logger;
            _apiUrl = config.GetSection("ApiUrl").Value;
        }

       
        /// <summary>
        /// Executing query on Data Set retreived from API
        /// </summary>
        /// <param name="albumUserId">Album User Id - Int?</param>
        /// <returns>IEnumerable of type AlbumPhotoModel</returns>
        public IEnumerable<AlbumPhoto> GetAlbumPhotos(int? albumUserId)
        {
            _logger.LogInformation("Getting Album Photo Data by API Call " + ((albumUserId != -99) ? ("for User Id " + albumUserId) : "for All User Id"));

            //Executing join query on Album and Photo Data set
            var listAlbumPhotos = from a in GetAllAlbums()
                                  join p in GetAllPhotos()
                                  on a.id equals p.albumId
                                  select new AlbumPhoto
                                  {
                                      albumId = a.id,
                                      albumUserId = a.userId,
                                      albumTitle = a.title,
                                      photoId = p.id,
                                      photoTitle = p.title,
                                      photoUrl = p.url,
                                      photoThumbnailUrl = p.thumbnailUrl
                                  };

            //Filtering Album User ID if valid albumUserId is passed
            listAlbumPhotos = (albumUserId == null) ? listAlbumPhotos : listAlbumPhotos.Where(r => r.albumUserId == albumUserId);

            _logger.LogInformation("API returned " + listAlbumPhotos.ToList().Count + " items " + ((albumUserId != -99) ? ("for User Id " + albumUserId) : "for All User Id"));

            return listAlbumPhotos;
        }

        /// <summary>
        /// Mapping Photo Data into List by Deserializing API json data
        /// </summary>
        /// <returns>List of Type Photo</returns>
        public List<Photo> GetAllPhotos()
        {
            List<Photo> lstPhoto = new List<Photo>();
            try
            {
                string responseString = GetDataFromAPI("photos");

                if (!string.IsNullOrEmpty(responseString))
                    lstPhoto = JsonConvert.DeserializeObject<List<Photo>>(responseString);
            }
            catch (Exception ex)
            {
                _logger.LogError("Photo Data mapping error due to " + ex.Message);
            }
            return lstPhoto;
        }

        /// <summary>
        /// Mapping Album Data into List by Deserializing API json data
        /// </summary>
        /// <returns>List of Type Album</returns>
        public List<Album> GetAllAlbums()
        {
            List<Album> lstAlbum = new List<Album>();
            try
            {
                string responseString = GetDataFromAPI("albums");

                if (!string.IsNullOrEmpty(responseString))
                    lstAlbum = JsonConvert.DeserializeObject<List<Album>>(responseString);

            }
            catch (Exception ex)
            {
                _logger.LogError("Album Data mapping error due to " + ex.Message);
            }
            return lstAlbum;
        }

        /// <summary>
        /// Calling API to get data
        /// </summary>
        /// <param name="dataEntity">Type of Data to be retreived</param>
        /// <returns>Json string</returns>
        private string GetDataFromAPI(string dataEntity)
        {
            string responseString = "";
            try
            {
                //API Call to get data
                var request = (HttpWebRequest)WebRequest.Create(_apiUrl + dataEntity);
                request.Method = "GET";
                request.ContentType = "application/json";

                using (var response1 = request.GetResponse())
                {
                    using (var reader = new StreamReader(response1.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("No Data found due to error " + ex.Message + " from API - " + dataEntity);
            }
            return responseString;
        }
    }
}
