using DataAccessLayer.DataEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace DataAccessLayer
{
    public class BenivoInitializer : DropCreateDatabaseIfModelChanges<BenivoContext> //CreateDatabaseIfNotExists
    {
        protected override void Seed(BenivoContext context)
        {
            var user = new User
            {
                Name = "Test1",
                Password = "Asdasd1".GetMD5()
            };

            var stories = new Story[]
            {
                new Story
                {
                    Title = "First Story",
                    Description = "First Storys Description",
                    Content = "First Storys Content",
                    PostedOn = DateTime.Now,
                    Creator = user
                },
                new Story
                {
                    Title = "Second Story",
                    Description = "Second Storys Description",
                    Content = "Second Storys Content",
                    PostedOn = DateTime.Now,
                    Creator = user
                }
            };

            var groups = new Group[]
            {
                new Group
                {
                    Name = "First Group",
                    Description = "First Groups Description",
                    Members = new User[] {
                        user
                    },
                    Stories = stories
                },
                new Group
                {
                    Name = "Second Group",
                    Description = "Second Groups Description",
                    Members = new User[] {
                        user
                    },
                    Stories = new Story[] {
                        stories.FirstOrDefault()
                    }
                }
            };

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Groups.AddOrUpdate(
              m => m.Name,
              groups
            );

            context.SaveChanges();
        }
    }
}
