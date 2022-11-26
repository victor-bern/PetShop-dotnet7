using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PetShop.Config;
using PetShop.Domain.Entities;

namespace PetShop.Utils;

using BCrypt.Net;

public static class AuthUtil
{
    public static string GetToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        Console.WriteLine(user.Role.ToString());
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async static Task<string> HashPass(string password)
    {
        return await Task.Run(() =>
        {
            var salt = BCrypt.GenerateSalt(8);
            return BCrypt.HashPassword(password);
        });
    }

    public static bool VerifyPass(string hashedPass, string password) => BCrypt.Verify(password, hashedPass);
    
}