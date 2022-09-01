using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlbumExp
{
    /// <summary>
    /// Interface for Authentication manager class having method to authentcate
    /// </summary>
    public interface IJWTAuthenticationManager
    {
        public string Authenticate(string userID, string password);
    }
}
