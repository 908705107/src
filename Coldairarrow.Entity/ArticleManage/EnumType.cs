namespace Coldairarrow.Entity.ArticleManage
{
    /// <summary>
    /// 枚举类型
    /// </summary>
    public class EnumType
    {
        /// <summary>
        /// 系统角色类型
        /// </summary>
        public enum IsTopEnum
        {
            精品 = 1,
            置顶 = 2
        }

        /// <summary>
        /// 是否发布，0未发布,1已发布
        /// </summary>
        public enum IsRelease
        {
            未发布 = 0,
            已发布 = 1
        }
    }
}
