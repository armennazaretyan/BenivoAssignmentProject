﻿using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccessLayer.DataEntities;
using DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.InfoServices
{
    public class GroupService : BaseService, IGroupService
    {
        public GroupService(IUnitOfWork unit)
            : base(unit)
        {
        }

        public bool Add(GroupModel group, int userId)
        {
            try
            {
                if (group != null)
                {
                    User user = unit.UserRepository.GetByID(userId);

                    unit.GroupRepository.Insert(new Group
                    {
                        Name = group.Name,
                        Description = group.Description,
                        Members = (user == null) ? null : new User[]
                                                            {
                                                                user
                                                            }
                    });

                    unit.Save();

                    return true;
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public bool Edit(GroupModel group)
        {
            try
            {
                if (group != null)
                {
                    unit.GroupRepository.Edit(new Group
                    {
                        Id = group.Id,
                        Name = group.Name,
                        Description = group.Description
                    });

                    unit.Save();

                    return true;
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public bool AddMember(int groupId, int userId)
        {
            try
            {
                var user = unit.UserRepository.GetByID(userId);

                if (user != null)
                {
                    if (unit.GroupRepository.AddMember(groupId, user))
                    {
                        unit.Save();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public bool RemoveMember(int groupId, int userId)
        {
            try
            {
                var user = unit.UserRepository.GetByID(userId);

                if (user != null)
                {
                    if (unit.GroupRepository.RemoveMember(groupId, user))
                    {
                        unit.Save();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public IEnumerable<GroupModel> Get(int? id)
        {
            try
            {
                var groups = id.HasValue ? unit.GroupRepository.GetDetailsByUserId(id.Value) : unit.GroupRepository.GetDetails();

                if (groups != null)
                {
                    return groups.Select(s => (GroupModel)s);
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public GroupModel GetDetails(int id)
        {
            try
            {
                var group = unit.GroupRepository.GetDetails(id);

                if (group != null)
                {
                    return (GroupModel)group;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
