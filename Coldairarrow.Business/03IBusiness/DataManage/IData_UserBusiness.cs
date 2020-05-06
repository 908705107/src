using Coldairarrow.Entity.DataManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.DataManage
{
    public interface IData_UserBusiness
    {
        Task<List<Data_User>> GetDataListAsync(Pagination pagination, string condition, string keyword);
        Task<Data_User> GetTheDataAsync(string id);
        Task AddDataAsync(Data_User data);
        Task UpdateDataAsync(Data_User data);
        Task DeleteDataAsync(List<string> ids);
    }
}