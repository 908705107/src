using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ArticleManage
{
    /// <summary>
    /// 文章分类表
    /// </summary>
    [Table("Article_ArticleCateGory")]
    public class Article_ArticleCateGory
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
        /// 文章ID
        /// </summary>
        public String ArtileID { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public String CategoryID { get; set; }

    }
}