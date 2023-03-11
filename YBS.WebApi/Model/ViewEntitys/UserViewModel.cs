using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewEntitys
{
    public class UserViewModel
    {
        public long? Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string? Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string? Tel { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public long? RoleId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string? Role { get; set; }
    }
}
