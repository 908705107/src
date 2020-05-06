using Coldairarrow.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ArticleManage
{
    /// <summary>
    /// 文章主表
    /// </summary>
    [Table("Article_Info")]
    public class Article_Info
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
        /// 文章标题
        /// </summary>
        public String Tittle { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public String Author { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public Int32? Supports { get; set; }

        /// <summary>
        /// Banner图
        /// </summary>
        public String BannerPic { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Int32? State { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public Int32? Code { get; set; }

        /// <summary>
        /// 浏览总量
        /// </summary>
        public Int32? ViewCounts { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? ReleaseTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifiedTime { get; set; }
        /// <summary>
        /// 是否置顶或精品 1置顶，2精品
        /// </summary>
        public Boolean IsToporQuality { get; set; }
        /// <summary>
        /// 是否开启评论 默认是
        /// </summary>
        public Boolean IsComment { get; set; }


    }
}