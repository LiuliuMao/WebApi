using Common;
using IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.ViewEntitys;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Auth
{
    //自行添加policy
    public class AuthServer : IAuthServer
    {
        private readonly IUserInfoService _userInfoService;
        private readonly AuthSetting _authSetting;

        public AuthServer(IOptionsMonitor<AuthSetting> authSetting,
            IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
            _authSetting = authSetting.CurrentValue;
        }


        //public async Task<AuthResult> CreateAuthentication(UserInfo user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_authSetting.Secret);
        //    var now = DateTime.Now;
        //    var claims = new List<Claim> {
        //     new Claim(ClaimTypes.Name, user.Account),
        //     new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
        //     new Claim(ClaimTypes.Expiration, now.AddSeconds(_authSetting.TokenLifetime.TotalSeconds).ToString())};

        //    var token = new JwtSecurityToken
        //    (
        //        claims: claims,
        //        issuer: _authSetting.Issuer,
        //        audience: _authSetting.Audience,
        //        notBefore: now,
        //        expires: now.Add(_authSetting.TokenLifetime),
        //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    );

        //    var strToken = tokenHandler.WriteToken(token);

        //    return new AuthResult
        //    {
        //        success = true,
        //        token = strToken,
        //        ExpirySecond = _authSetting.TokenLifetime.TotalSeconds //过期时间
        //    };
        //}
        public AuthResult CreateAuthentication(UserViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSetting.Secret);
            var claims = new List<Claim> {
                    //new Claim(JwtRegisteredClaimNames.Typ, "Supervisor"),
                     new Claim(ClaimTypes.Role, user.Role),
             new Claim(ClaimTypes.Name, user.Account),
             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())};
            var token = new JwtSecurityToken
            (
                claims: claims,
                issuer: _authSetting.Issuer,
                audience: _authSetting.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_authSetting.AccessExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );

            //var token = new JwtSecurityToken(_authSetting.Issuer, _authSetting.Audience, claims,
            //  expires: DateTime.Now.AddMinutes(_authSetting.AccessExpiration),
            //  signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));
            var strToken = tokenHandler.WriteToken(token);

            return new AuthResult
            {
                success = true,
                token = strToken,
                ExpirySecond = _authSetting.AccessExpiration //过期时间
            };
        }

        public TokenModelJwt SerializeJwt(string oldToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            try
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(oldToken);

                bool isHave = jwtToken.Payload.ContainsKey(ClaimTypes.NameIdentifier);
                object uid = default;
                if (isHave)
                {
                    jwtToken.Payload.TryGetValue(ClaimTypes.NameIdentifier, out uid);
                }
                return new TokenModelJwt()
                {
                    UserId = uid.ToString()
                };
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
