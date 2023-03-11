using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    [Table("CompanyInfo")]
    public class CompanyInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        [MaxLength(25), Required]
        public string Name { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary>
        [Column(TypeName = "text")]
        public string Address { get; set; }
        /// <summary>
        /// 单位联系电话
        /// </summary>
        [MaxLength(25), Required]
        public string Tel { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }
    }
}
