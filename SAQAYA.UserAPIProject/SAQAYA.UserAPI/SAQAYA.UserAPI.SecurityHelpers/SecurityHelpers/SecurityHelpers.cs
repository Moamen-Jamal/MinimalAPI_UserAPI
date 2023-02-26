using Microsoft.IdentityModel.Tokens;
using SAQAYA.UserAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace SAQAYA.UserAPI
{
    public class SecurityHelpers
    {
        #region BuildToken
        /// <summary>
        /// 1- Create Claims for AccessToken
        /// 2- Create securityKey, credentials and tokenDescriptor
        /// 3- Generate new AccessToken
        /// </summary>
        /// <param name="key" type="string"></param>
        /// <param name="user" type="UserModel"></param>
        /// <returns name="AccessToken" type="string"></returns>
        public static string BuildToken(string key, UserModel user)
        {
            var claims = new[]
            {
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Name, user.LastName),
                new Claim(ClaimTypes.Email, user.Email)
             };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        #endregion

        #region GenerateId
        /// <summary>
        /// 1- Generate SHA1 for Id
        /// 2- Create MD5
        /// 3- Generate new Id
        /// </summary>
        /// <param name="input" type="string"></param>
        /// <param name="userSecretID" type="string"></param>
        /// <returns name="Id" type="string"></returns>
        public static string GenerateId(string input, string userSecretID)
        {
            var inputSecretID = input + userSecretID;
            inputSecretID = GenerateSHA1(inputSecretID);
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(inputSecretID);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion

        #region Helpers

        //This private method is used to generate SHA1
        private static string GenerateSHA1(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);
            return HexStringFromBytes(hashBytes);
        }

        //This method is used to make HexStringFromBytes
        private static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
        #endregion

    }
}
