using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DataManage
{
    /// <summary>
    /// 标签表
    /// </summary>
    [Table("Data_Tag")]
    public class Data_Tag
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public String TagName { get; set; }

        /// <summary>
        /// 标签别名
        /// </summary>
        public String TabAlias { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

    }
}