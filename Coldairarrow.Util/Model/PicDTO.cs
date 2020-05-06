namespace Coldairarrow.Util
{
    /// <summary>
    /// Ajax请求结果
    /// </summary>
    public class PicDTO
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 绝对路径
        /// </summary>
        public string thumbUrl { get; set; }
        /// <summary>
        /// 相对路径
        /// </summary>
        public string path { get; set; }
        ///// <summary>
        ///// 文章ID
        ///// </summary>
        //public string ArticleID { get; set; }
    }
}
