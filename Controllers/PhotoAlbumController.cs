using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoAlbumExp.Business;
using PhotoAlbumExp.Model;
using Microsoft.AspNetCore.Authorization;

namespace PhotoAlbumExp.Controllers
{
    /// <summary>
    /// API Controller Class
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class PhotoAlbumController : ControllerBase
    {
        private readonly ILogger<PhotoAlbumController> _logger;
        private readonly IPhotoAlbum _photoAlbumHandler;
        private readonly IJWTAuthenticationManager _jWTAuthenticationManager;

        /// <summary>
        /// Injecting Logge, Business Handler and authentication manager  
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="photoAlbumHandler"></param>
        public PhotoAlbumController(ILogger<PhotoAlbumController> logger, IPhotoAlbum photoAlbumHandler, IJWTAuthenticationManager jWTAuthenticationManager)
        {
            _logger = logger;
            _photoAlbumHandler = photoAlbumHandler;
            _jWTAuthenticationManager = jWTAuthenticationManager;
        }

        /// <summary>
        /// Action method to Get Album and Photo items
        /// </summary>
        /// <param name="albumUserId">Album User Id - int?</param>
        /// <returns>IEnumerable of type AlbumPhotoModel</returns>
        [HttpGet("{albumUserId}")]
        public IEnumerable<AlbumPhoto> GetAlbumPhotos(int albumUserId = -99)
        {
            IEnumerable<AlbumPhoto> lstAlbumPhotos = Enumerable.Empty<AlbumPhoto>();
            try
            {
                //Valdating if User ID = 0 and throwing Out of Range exception
                if (albumUserId == 0)
                    throw new ArgumentOutOfRangeException();

                _logger.LogInformation("Fetching Album's Photo List " + ((albumUserId != -99) ? ("for User Id " + albumUserId) : "for All User Id"));

                //Calling Business Level Function
                lstAlbumPhotos = _photoAlbumHandler.GetAlbumPhotos(albumUserId);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogError("User Id is not valid");
            }
            catch (Exception ex)
            {
                _logger.LogError("API Call unsuccessful due to error " + ex.Message);
            }

            return lstAlbumPhotos;
        }

        /// <summary>
        /// Authenticating service request : returning token if user credential match else sending null
        /// </summary>
        /// <param name="userCred">user credential</param>
        /// <returns>returning token</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = _jWTAuthenticationManager.Authenticate(userCred.Username, userCred.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
