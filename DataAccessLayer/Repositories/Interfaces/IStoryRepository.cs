using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IStoryRepository : IGenericRepository<Story>
    {
        void Edit(Story story);
        Story GetDetails(int id);
        IEnumerable<Story> GetByUserId(int userId);
        IEnumerable<Story> GetByGroupId(int groupId);
    }
}
