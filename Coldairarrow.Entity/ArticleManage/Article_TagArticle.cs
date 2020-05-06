using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ArticleManage
{
    /// <summary>
    /// 文章标签表
    /// </summary>
    [Table("Article_TagArticle")]
    public class Article_TagArticle
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
        /// 博文ID
        /// </summary>
        public String ArticleID { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        public String TagID { get; set; }

    }
}