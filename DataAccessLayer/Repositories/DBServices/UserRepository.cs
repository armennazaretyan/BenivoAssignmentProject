using DataAccessLayer.DataEntities;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.DBServices
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BenivoContext context)
            : base(context)
        {
            this.context = context;
        }

        public User Get(string name, string password)
        {
            return context.Users.FirstOrDefault(m => m.Name == name && m.Password == password);
        }

        public User Get(string name)
        {
            return context.Users.FirstOrDefault(m => m.Name == name);
        }
    }
}
