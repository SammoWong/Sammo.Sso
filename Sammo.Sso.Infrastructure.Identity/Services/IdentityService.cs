using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sammo.Sso.Common.Exceptions;
using Sammo.Sso.Domain.Constants;
using Sammo.Sso.Domain.Entities;
using Sammo.Sso.Domain.Interfaces;
using Sammo.Sso.Infrastructure.Identity.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sammo.Sso.Infrastructure.Identity.Services
{
    public class IdentityService
    {
        //用于从AuthenticationTicket中获取Audience信息
        private const string AudiencePropertyKey = "aud";

        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Application> _applicationRepository;

        public IdentityService(IConfiguration configuration, IRepository<User> userRepository, IRepository<Application> applicationRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _applicationRepository = applicationRepository;
        }

        public async Task<string> Authenticate(AuthenticateInput input)
        {
            //var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == input.Email && u.Password == input.Password);
            //if (user == null)
            //    throw new KnownException();

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
            };
            var claimsIdentity = new[] { new ClaimsIdentity(claims) };
            var properties = new AuthenticationProperties();
            properties.Parameters.Add(AudiencePropertyKey, "Sso");
            properties.ExpiresUtc = DateTime.Now.AddHours(2);
            return await CreateAccessToken(new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), properties, ""));
        }

        private async Task<string> CreateAccessToken(AuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new KnownException(ErrorCode.Default);

            string audienceId = ticket.Properties.Parameters.ContainsKey(AudiencePropertyKey)
                ? ticket.Properties.GetParameter<string>(AudiencePropertyKey)
                : null;

            if (string.IsNullOrEmpty(audienceId))
                throw new KnownException(ErrorCode.Default);

            var audience = await _applicationRepository.FirstOrDefaultAsync(a => a.ClientId == audienceId);
            var expireTime = DateTime.Now.AddHours(2);
            var secret = audience.ClientSecrets;
            var expires = ticket.Properties.ExpiresUtc;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken
            (
                //颁发者
                issuer: "SSO",
                //接收者
                audience: audience.ClientName,
                //过期时间
                expires: expires.Value.DateTime,
                //签名证书
                signingCredentials: creds,
                //自定义参数
                claims: ticket.Principal.Claims
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
