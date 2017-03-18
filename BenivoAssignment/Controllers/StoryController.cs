using BenivoAssignment.Models;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace BenivoAssignment.Controllers
{
    [Authorize]
    public class StoryController : BaseController
    {
        private IStoryService storyService;
        private IGroupService groupService;


        public StoryController(IStoryService storyService, IGroupService groupService)
        {
            this.storyService = storyService;
            this.groupService = groupService;
        }

        //
        // GET: /Story
        public ActionResult Index()
        {
            var model = storyService.GetByUser(CurrentUserId).Select(s => new StoryViewModel
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Content = s.Content,
                PostedOn = s.PostedOn,
                IsEditable = true
            });

            return View(model);
        }

        //
        // GET: /Story/Create        
        [HttpGet]
        public ActionResult Create()
        {
            var model = new StoryCreateViewModel
            {
                GroupListItems = GetGroupsListItems(),
            };

            return View(model);
        }

        //
        // POST: /Story/Create
        [HttpPost]
        public ActionResult Create(StoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = storyService.Add(new StoryModel
                {
                    Title = model.Title,
                    Content = model.Content,
                    Description = model.Description,
                    CreatorId = CurrentUserId,
                    GroupIds = model.GroupIds,
                });

                if (result)
                {
                    return RedirectToAction("Index", "Story");
                }
                else
                {
                    ModelState.AddModelError("", "Story creation failed");
                }
            }

            model.GroupListItems = GetGroupsListItems();
            return View(model);
        }

        //
        // GET: /Story/Edit    
        public ActionResult Edit(int id)
        {
            var story = storyService.GetDetails(id);

            if (story != null && story.CreatorId == CurrentUserId)
            {
                var model = new StoryEditViewModel
                {
                    Id = story.Id,
                    Title = story.Title,
                    Description = story.Description,
                    Content = story.Content,
                    GroupIds = story.Groups.Select(s => s.Id).ToArray(),
                    GroupListItems = GetGroupsListItems()
                };

                return View(model);
            }

            else return RedirectToAction("Index", "Story");
        }


        //
        // POST: /Story/Edit
        [HttpPost]
        public ActionResult Edit(StoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = storyService.Edit(new StoryModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    Description = model.Description,
                    GroupIds = model.GroupIds,
                });

                if (result)
                {
                    return RedirectToAction("Index", "Story");
                }
                else
                {
                    ModelState.AddModelError("", "Story creation failed");
                }
            }

            model.GroupListItems = GetGroupsListItems();
            return View(model);
        }


        //
        // GET: /Story/Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var story = storyService.GetDetails(id);

            if (story != null)
            {
                var model = new StoryDetailsViewModel
                {
                    Id = story.Id,
                    Title = story.Title,
                    Description = story.Description,
                    Content = story.Content,
                    PostedOn = story.PostedOn,
                    IsEditable = story.CreatorId == CurrentUserId,
                    Creator = new UserViewModel
                    {
                        Id = story.Creator.Id,
                        Name = story.Creator.Name
                    },
                    Groups = story.Groups.Select(s => new GroupViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description
                    }).ToArray()
                };

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Story was not found");
            }

            return RedirectToAction("Index", "Story");
        }
        public IEnumerable<SelectListItem> GetGroupsListItems()
        {
            return groupService.Get().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
        }


    }
}