using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Coldairarrow.Business.DataManage.Data_CategoryBusiness;

namespace Coldairarrow.Business.DataManage
{
    public interface IData_CategoryBusiness
    {
        Task<List<Data_CategoryTreeDTO>> GetDataTreeListAsync(string condition, string keyword, bool isall);
        Task<List<Data_Category>> GetDataListAsync(Pagination pagination, string condition, string keyword);
        Task<Data_Category> GetTheDataAsync(string id);
        Task AddDataAsync(Data_Category data);
        Task UpdateDataAsync(Data_Category data);
        Task DeleteDataAsync(List<string> ids);
    }
}