using DataAccessLayer.DataEntities;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.DBServices
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(BenivoContext context)
            : base(context)
        {
            this.context = context;
        }

        public void Edit(Group group)
        {
            Group updatedGroup = GetByID(group.Id);

            updatedGroup.Name = group.Name;
            updatedGroup.Description = group.Description;

            context.Entry(updatedGroup).State = EntityState.Modified;
        }

        public bool AddMember(int groupId, User user)
        {
            Group group = GetByID(groupId);

            if (group != null && user != null && !group.Members.Any(a => a.Id == user.Id))
            {
                group.Members.Add(user);
                context.Entry(group).State = EntityState.Modified;
                return true;
            }
            return false;
        }

        public bool RemoveMember(int groupId, User user)
        {
            Group group = GetByID(groupId);

            if (group != null && user != null && group.Members.Any(a => a.Id == user.Id))
            {
                var result = group.Members.Remove(user);
                context.Entry(group).State = EntityState.Modified;
                return result;
            }
            return false;
        }

        public IEnumerable<Group> GetDetails()
        {
            return context.Groups.Include(i => i.Members).Include(i => i.Stories);
        }

        public Group GetDetails(int id)
        {
            return context.Groups.Include(i => i.Members).Include(i => i.Stories).FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Group> GetDetailsByUserId(int userId)
        {
            User user = context.Users.Include(i => i.Groups.Select(s => s.Stories)).SingleOrDefault(m => m.Id == userId);
            return user != null ? user.Groups : null;
        }
    }
}
