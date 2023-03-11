using Model;
using Model.ViewEntitys;
using Model.ViewEntitys.QueryParameter;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public interface IUserInfoService:IBaseService<UserInfo>
    {

        Task<ApiResponse> Resgiter(UserViewModel user);
        Task<ApiResponse> LoginAsync(string Account, string Password);
        Task<ApiResponse> GetUsers(UserParameter parameter); 
    }
}
