using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portal.Domain.Core.Auth;
using Portal.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Services
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateUser(UserLoginModel userForAuth);

        Task<string> CreateToken();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection JwtSettings;
        private User _user;

        public AuthenticationService(
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            JwtSettings = _configuration.GetSection("JwtSettings");
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> ValidateUser(UserLoginModel userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
        }

        #region private methods

        private SigningCredentials GetSigningCredentials()
        {
            //var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var key = Encoding.UTF8.GetBytes(JwtSettings["key"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
                //issuer: JwtSettings.GetSection("validIssuer").Value,
                //audience: JwtSettings.GetSection("validAudience").Value,
                issuer: JwtSettings["validIssuer"],
                audience: JwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(JwtSettings["expires"])),
                //DateTime.Now.AddMinutes(Convert.ToDouble(JwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }

        #endregion private methods
    }
}