using Model.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("UserInfo")]
    public class UserInfo: BaseEntitys
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(25), Required]
        public string UserName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [MaxLength(25), Required]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(25), Required]
        public string Password { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [MaxLength(25), Required]
        public string Tel { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public long RoleId { get; set; }
    }
}
