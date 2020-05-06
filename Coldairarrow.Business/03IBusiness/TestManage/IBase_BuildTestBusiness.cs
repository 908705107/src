using Coldairarrow.Entity.TestManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.TestManage
{
    public interface IBase_BuildTestBusiness
    {
        Task<List<Base_BuildTest>> GetDataListAsync(Pagination pagination, string condition, string keyword);
        Task<Base_BuildTest> GetTheDataAsync(string id);
        Task AddDataAsync(Base_BuildTest data);
        Task UpdateDataAsync(Base_BuildTest data);
        Task DeleteDataAsync(List<string> ids);
    }
}