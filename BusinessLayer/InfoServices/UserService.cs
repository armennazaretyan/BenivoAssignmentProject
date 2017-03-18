using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer.DataEntities;
using DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace BusinessLayer.InfoServices
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unit)
            : base(unit)
        {
        }

        public bool RegisterUser(string username, string password)
        {
            try
            {
                unit.UserRepository.Insert(new User
                {
                    Name = username,
                    Password = password.GetMD5()
                });

                unit.Save();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public UserModel GetUser(object providerUserKey)
        {
            try
            {
                User user = unit.UserRepository.GetByID((int)providerUserKey);

                if (user != null)
                {
                    return new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public UserModel GetUser(string username, string password)
        {
            try
            {
                User user = unit.UserRepository.Get(username, password.GetMD5());

                if (user != null)
                {
                    return new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public UserModel GetUser(string username)
        {
            try
            {
                User user = unit.UserRepository.Get(username);

                if (user != null)
                {
                    return new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }
    }
}
