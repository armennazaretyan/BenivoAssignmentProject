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
    public class StoryRepository : GenericRepository<Story>, IStoryRepository
    {
        public StoryRepository(BenivoContext context)
            : base(context)
        {
            this.context = context;
        }
        public void Edit(Story story)
        {
            Story updatedStory = GetByID(story.Id);

            // clearing old groups
            updatedStory.Groups.Clear();
            context.SaveChanges();

            updatedStory.Title = story.Title;
            updatedStory.Description = story.Description;
            updatedStory.Content = story.Content;
            updatedStory.Groups = story.Groups;

            context.Entry(updatedStory).State = EntityState.Modified;
        }

        public Story GetDetails(int id)
        {
            return context.Stories.Include(i => i.Creator).Include(i => i.Groups).FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Story> GetByUserId(int userId)
        {
            return context.Stories.Include(i => i.Creator).Where(w => w.CreatorId == userId);
        }

        public IEnumerable<Story> GetByGroupId(int groupId)
        {
            Group group = context.Groups.Include(i => i.Stories.Select(s => s.Creator)).SingleOrDefault(m => m.Id == groupId);
            return group != null ? group.Stories : null;
        }
    }
}
