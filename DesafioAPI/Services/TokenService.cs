using DesafioAPI.Models;
using DesafioAPI.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioAPI.Services
{
    public class TokenService
    {
        public static string GetToken(Usuario user)
        {
            var key = Encoding.ASCII.GetBytes(PrivateKeyJWT.Key);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Nome.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                },
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
