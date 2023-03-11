using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    [Table("MenuInfo")]
    public class MenuInfo : BaseEntitys
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(25), Required]
        public string Name { get; set; }
        /// <summary>
        /// 菜单命名空间
        /// </summary>
        [MaxLength(25), Required]
        public string NameSpace { get; set; }
        /// <summary>
        /// 上级菜单ID
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }
    }
}
