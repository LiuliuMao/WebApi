using Common;
using Common.Cache;
using Common.Helper;
using IRepository;
using IService;
using Model;
using Model.ViewEntitys;
using Model.ViewEntitys.QueryParameter;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public UserInfoService(IDBContextFactory contextFactory, ISqlServerDBContext sqlServerDBContext) : base(contextFactory, sqlServerDBContext)
        {

        }
        public async Task<ApiResponse> Resgiter(UserViewModel user)
        {
            try
            {
                var userModel = this.FirstOrDefault(x => x.Account.Equals(user.Account));

                if (userModel != null)
                    return new ApiResponse($"当前账号:{user.Account}已存在,请重新注册！");
                UserInfo model = new UserInfo();
                string pwd = EncodeHelper.AES(user.Password);
                model.Account = user.Account;
                model.Password = pwd;
                model.Tel = user.Tel;
                model.RoleId = (int)user.RoleId;
                model.UserName = user.UserName;
                model.CreateTime = DateTime.Now;
                var userdata = this.Add(model);
                if (userdata != null)
                    return new ApiResponse(true, userdata);

                return new ApiResponse("注册失败,请稍后重试！");
            }
            catch (Exception ex)
            {
                return new ApiResponse("注册账号失败：" + ex.Message);
            }
        }
        public async Task<ApiResponse> LoginAsync(string Account, string Password)
        {
            try
            {
                string pwd = EncodeHelper.AES(Password);
                var model = this.FirstOrDefault(x => x.Account == Account && x.Password == pwd);
                if (model == null)
                    return new ApiResponse("账号或密码错误,请重试！");
                return new ApiResponse(true, model);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, "登录失败：" + ex.Message);
            }
        }
        public async Task<ApiResponse> GetUsers(UserParameter userParameter)
        {
            try
            {
                int count = 0;
                Expression<Func<UserInfo, bool>> expression = p => p.UserName.Contains(userParameter.UserName) && p.Account.Contains(userParameter.Account);
                if (userParameter.RoleId > 0)
                    expression = p => p.UserName.Contains(userParameter.UserName) && p.Account.Contains(userParameter.Account) && p.RoleId == userParameter.RoleId;
                var pagedata = this.Where(expression, t => t.UpdateTime, userParameter.PageIndex, userParameter.PageSize, out count, true);
                PagedList<UserInfo> data = new PagedList<UserInfo>();
                data.PageIndex = userParameter.PageIndex;
                data.PageSize = userParameter.PageSize;
                data.TotalCount = count;
                data.Items = new List<UserInfo>();
                if (pagedata != null)
                {
                    var list = pagedata.ToList();
                    foreach (var item in pagedata)
                    {
                        data.Items.Add(item);
                    }
                }
                return new ApiResponse(true, data);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, ex.Message);
            }
        }
    }
}
