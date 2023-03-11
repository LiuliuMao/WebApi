using Common.Cache;
using Common;
using IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApi.Auth;

namespace WebApi.Controllers
{
    public class MenuInfoController : BaseController
    {
        private readonly IUserInfoService _service;
        private readonly IAuthServer _authServer;
        private readonly IRedisCache _cache;
        private readonly AuthSetting _authSetting;
        public MenuInfoController(IUserInfoService service, IAuthServer authServer, IRedisCache cache, IOptions<AuthSetting> authSetting)
        {
            _service = service;
            _authServer = authServer;
            _cache = cache;
            _authSetting = authSetting.Value;
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserAll")]
        [Authorize]
        //[AllowAnonymous]
        public IActionResult GetUserAll()
        {
            return Ok(JsonConvert.SerializeObject(_service.GetAll()));
        }
    }
}
