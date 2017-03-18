using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        bool RegisterUser(string username, string password);
        UserModel GetUser(object providerUserKey);
        UserModel GetUser(string username);
        UserModel GetUser(string username, string password);
    }
}
