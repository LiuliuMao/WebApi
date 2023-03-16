using Common;
using Common.Cache;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;
using Newtonsoft.Json;
using Shared;
using Model.ViewEntitys;
using Model.ViewEntitys.QueryParameter;

namespace WebApi.Controllers
{
    public class UserInfoController : BaseController
    {
        private readonly IUserInfoService _service;
        private readonly IAuthServer _authServer;
        private readonly IRedisCache _cache;
        private readonly AuthSetting _authSetting;
        public UserInfoController(IUserInfoService service, IAuthServer authServer, IRedisCache cache, IOptions<AuthSetting> authSetting)
        {
            _service = service;
            _authServer = authServer;
            _cache = cache;
            _authSetting = authSetting.Value;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ApiResponse> Login([FromBody] UserViewModel userInfo)
        {
            if(userInfo==null)
                return new ApiResponse("数据为空 ");
            var result = await _service.LoginAsync(userInfo.Account, userInfo.Password);
            if (result.Status)
            {
                var cacheToken = _cache.GetValue(userInfo.Account);
                if (!string.IsNullOrWhiteSpace(cacheToken))
                {
                    return result;
                }
                var model = result.Data as UserInfo;
                var response = _authServer.CreateAuthentication(model);
                _cache.Set(model.Id.ToString(), response.token, new TimeSpan(0,0, _authSetting.AccessExpiration));
                result.Data = new
                {
                    UserName = model.UserName,
                    Token = response.token,
                };
            }
            return result;
        }
        [HttpPost("Resgiter")]
        [AllowAnonymous]
        public async Task<ApiResponse> Resgiter([FromBody] UserViewModel userInfo)
        {
            return await _service.Resgiter(userInfo);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<ApiResponse> RefreshToken(RefreshCredentials refreshCredentials)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(false, BadRequest(ModelState));
            }

            var JwtModel = _authServer.SerializeJwt(refreshCredentials.OldToken);
            if (JwtModel == null)
            {
                return new ApiResponse("token无效，请重新登录！");
            }
            var userModel = await _service.AysGetById(int.Parse(JwtModel.UserId));

            if (userModel != null)
            {
                var cacheToken = _cache.GetValue(userModel.Id.ToString());
                if (string.IsNullOrWhiteSpace(cacheToken))
                {
                    return new ApiResponse("token已过期，请重新登录！");
                }

                var response = _authServer.CreateAuthentication(userModel);
                if (!response.success)
                {
                    return new ApiResponse(false, BadRequest(response.message));
                }
                _cache.Set(userModel.Id.ToString(), response.token, new TimeSpan(0, 0, _authSetting.AccessExpiration));

                return new ApiResponse(true, response);
            }
            else
            {
                return new ApiResponse(false, "认证失败");
            }
        }

        /// <summary>
        /// token强制失效
        /// </summary>
        /// <param name="refreshCredentials"></param>
        /// <returns></returns>
        [HttpPost("Mandatoryoffline")]
        [Authorize(Roles = "超级管理员")]
        public async Task<ApiResponse> Mandatoryoffline(RefreshCredentials refreshCredentials)
        {
            if (!ModelState.IsValid)
            {
                return new ApiResponse(false, BadRequest(ModelState));
            }
            var JwtModel = _authServer.SerializeJwt(refreshCredentials.OldToken);
            if (JwtModel != null)
            {
                _cache.Remove(JwtModel.UserId);
            }
            return new ApiResponse(true, "token强制失效成功！");
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetUsers")]
        [AllowAnonymous]
        //[Authorize("Supervisor")]
        public async Task<ApiResponse> GetUsers([FromBody] UserParameter parameter)
        {
            return await _service.GetUsers(parameter);
        }
    }
}
