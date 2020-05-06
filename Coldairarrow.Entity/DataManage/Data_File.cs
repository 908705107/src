using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.DataManage
{
    /// <summary>
    /// 文件表
    /// </summary>
    [Table("Data_File")]
    public class Data_File
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
        /// 父ID
        /// </summary>
        public String Pid { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        public String FileID { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public String CategoryID { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public String Tag { get; set; }

        /// <summary>
        /// 0不是 1是文件夹
        /// </summary>
        public Int32? IsFolder { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public String Size { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public String FileName { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public String FileExtension { get; set; }

        /// <summary>
        /// 相对路径
        /// </summary>
        public String FilePath { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Int32? State { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public String FileAlias { get; set; }
        /// <summary>
        /// 文章ID
        /// </summary>
        public string ArticleID { get; set; }

    }
}