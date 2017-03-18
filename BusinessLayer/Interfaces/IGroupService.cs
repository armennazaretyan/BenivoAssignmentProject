using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IGroupService
    {
        bool Add(GroupModel group, int userId);
        bool Edit(GroupModel group);
        bool AddMember(int groupId, int userId);
        bool RemoveMember(int groupId, int userId);

        /// <param name="userId">
        /// returns groups for current user
        /// leave empty if get all groups
        /// </param>
        IEnumerable<GroupModel> Get(int? userId = null);
        GroupModel GetDetails(int id);
    }
}
