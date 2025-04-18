using API.Common.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Common.Application.JwtTokens
{
    public class GenerateJwtToken(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        private readonly UserManager<AppUser> userManager = userManager;
        private readonly IConfiguration configuration = configuration;

        public async Task<string> JwtTokenGenerate(AppUser user)
        {
            //var userRoles = await this.userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim("Id", user.Id.ToString()),
                new Claim("name", user.UserName!),  
                new Claim(ClaimTypes.Name, user.UserName!),  
                new Claim("FullName", user.FirstName + " " + user.LastName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: this.configuration["Jwt:Issuer"],
                audience: this.configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }
    }
}