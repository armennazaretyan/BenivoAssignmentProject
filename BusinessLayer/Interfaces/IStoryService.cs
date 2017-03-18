using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IStoryService
    {
        bool Add(StoryModel story);
        bool Edit(StoryModel story);
        StoryModel GetDetails(int id);
        IEnumerable<StoryModel> GetByUser(int userId);
        IEnumerable<StoryModel> GetByGroup(int groupId);
    }
}
