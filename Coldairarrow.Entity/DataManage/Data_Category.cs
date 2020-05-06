using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DataManage
{
    /// <summary>
    /// 分类表
    /// </summary>
    [Table("Data_Category")]
    public class Data_Category
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
        /// 分类名称
        /// </summary>
        public String CategoryName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Int32? Sort { get; set; }

        /// <summary>
        /// 分类类型 
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public String Pid { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String Remark { get; set; }

    }
}