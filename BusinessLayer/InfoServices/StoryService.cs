using BusinessLayer.Interfaces;
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
    public class StoryService : BaseService, IStoryService
    {
        public StoryService(IUnitOfWork unit)
            : base(unit)
        {
        }

        public bool Add(StoryModel story)
        {
            try
            {
                if (story != null && story.GroupIds.Count() > 0)
                {
                    unit.StoryRepository.Insert(new Story
                    {
                        Title = story.Title,
                        Description = story.Description,
                        Content = story.Content,
                        CreatorId = story.CreatorId,
                        PostedOn = DateTime.UtcNow,
                        Groups = story.GroupIds.Select(s => unit.GroupRepository.GetByID(s)).ToArray()
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

        public bool Edit(StoryModel story)
        {
            try
            {
                if (story != null && story.GroupIds.Count() > 0)
                {
                    unit.StoryRepository.Edit(new Story
                    {
                        Id = story.Id,
                        Title = story.Title,
                        Description = story.Description,
                        Content = story.Content,
                        Groups = story.GroupIds.Select(s => unit.GroupRepository.GetByID(s)).ToArray()
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

        public IEnumerable<StoryModel> GetByUser(int userId)
        {
            try
            {
                var stories = unit.StoryRepository.GetByUserId(userId);

                if (stories != null)
                {
                    return stories.Select(s => (StoryModel)s);
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public IEnumerable<StoryModel> GetByGroup(int groupId)
        {
            try
            {
                var stories = unit.StoryRepository.GetByGroupId(groupId);

                if (stories != null)
                {
                    return stories.Select(s => (StoryModel)s);
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }


        public StoryModel GetDetails(int id)
        {
            try
            {
                Story story = unit.StoryRepository.GetDetails(id);

                if (story != null)
                {
                    return (StoryModel)story;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
