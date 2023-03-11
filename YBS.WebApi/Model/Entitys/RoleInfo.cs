using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    [Table("RoleInfo")]
    public class RoleInfo : BaseEntitys
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(25), Required]
        public string Name { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }
    }
}
