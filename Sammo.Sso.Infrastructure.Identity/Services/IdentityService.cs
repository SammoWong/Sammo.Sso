using IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sammo.Sso.Domain.Constants;
using Sammo.Sso.Domain.Entities;
using Sammo.Sso.Domain.Interfaces;
using Sammo.Sso.Infrastructure.Exceptions;
using Sammo.Sso.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sammo.Sso.Infrastructure.Identity.Services
{
    public class IdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;

        public IdentityService(IConfiguration configuration, IRepository<User> userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> Authenticate(AuthenticateInput input)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == input.Email && u.Password == input.Password);
            if (user == null)
                throw new KnownException();

            return CreateAccessToken();
        }

        private string CreateAccessToken()
        {
            var expireTime = DateTime.Now.AddHours(2);
            var secret = _configuration.GetSection("Secret").Value;
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                    //颁发者
                    issuer: "SSO",
                    //接收者
                    audience: "SSO",
                    //过期时间
                    expires: expireTime,
                    //签名证书
                    signingCredentials: creds,
                    //自定义参数
                    claims: claims
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
