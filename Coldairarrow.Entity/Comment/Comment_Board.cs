using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Comment
{
    /// <summary>
    /// 留言板留言表
    /// </summary>
    [Table("Comment_Board")]
    public class Comment_Board
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
        /// 用户ID
        /// </summary>
        public String UserID { get; set; }

        /// <summary>
        /// 留言ID
        /// </summary>
        public String CommentID { get; set; }

        /// <summary>
        /// 父留言ID
        /// </summary>
        public String CommentPID { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public String CommentInfo { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public Int32? Supports { get; set; }

    }
}