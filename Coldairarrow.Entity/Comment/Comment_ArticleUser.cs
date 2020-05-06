using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Comment
{
    /// <summary>
    /// 文章用户评论表
    /// </summary>
    [Table("Comment_ArticleUser")]
    public class Comment_ArticleUser
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
        /// 评论ID
        /// </summary>
        public String CommentID { get; set; }

        /// <summary>
        /// 父评论ID
        /// </summary>
        public String CommentPID { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public String CommentInfo { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public String UserID { get; set; }

        /// <summary>
        /// 文章ID
        /// </summary>
        public String ArticleID { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int Supports { get; set; }

    }
}