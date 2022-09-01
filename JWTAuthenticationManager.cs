using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbumExp
{
    /// <summary>
    /// Authenticaton manager to authenticate a request
    /// </summary>
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        /// <summary>
        /// Data dictionary to hold authentication user credential
        /// </summary>
        private readonly IDictionary<string, string> users = new Dictionary<string, string> { { "test1", "password1" }, { "test2", "password2" } };
        private readonly string key;
        public JWTAuthenticationManager(string key)
        {
            this.key = key; //Passing key to encrypt the token
        }

        /// <summary>
        /// Maching passed user id and password: returning token if match else sending null
        /// </summary>
        /// <param name="username">user name</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public string Authenticate(string username, string password)
        {
            if (!users.Any(u => u.Key == username && u.Value == password))
                return null;

            //Creating JWT token
            var tokenHandler = new JwtSecurityTokenHandler(); // Create token handler
            var tokenKey = Encoding.ASCII.GetBytes(key); 
            //Creating actual token descriptor :define identity claim, set expiry, signing the token
            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
