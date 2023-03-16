using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    //Table==>对应数据库表名
    //Key==>主键
    //DatabaseGenerated.Identity ==>数据库自动增长列
    //Required==>非空
    public class BaseEntitys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        //public long CompanyId { get; set; }
    }
}
