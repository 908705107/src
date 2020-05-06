using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DataManage
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("Data_User")]
    public class Data_User
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
        /// 用户名称
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 0男1女
        /// </summary>
        public Int32? sex { get; set; }

        /// <summary>
        /// 用户类型  QQ用户，游客等
        /// </summary>
        public String UserType { get; set; }

    }
}