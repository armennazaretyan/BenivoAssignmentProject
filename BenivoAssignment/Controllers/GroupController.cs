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
    public class GroupController : BaseController
    {
        private IStoryService storyService;
        private IGroupService groupService;

        public GroupController(IStoryService storyService, IGroupService groupService)
        {
            this.storyService = storyService;
            this.groupService = groupService;
        }

        //
        // GET: /Group/
        public ActionResult Index()
        {
            var model = groupService.Get().Select(m => new GroupViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                MembersCount = m.Members.Count(),
                StoryesCount = m.Stories.Count(),
                IsJoined = m.Members.Any(l => l.Id == CurrentUserId),
            });

            return View(model.OrderByDescending(m => m.IsJoined));
        }

        //
        // GET: /Group/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Group/Create
        [HttpPost]
        public ActionResult Create(GroupCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = groupService.Add(new GroupModel
                {
                    Name = model.Name,
                    Description = model.Description
                }, CurrentUserId);

                if (result)
                {
                    return RedirectToAction("Index", "Group");
                }
                else
                {
                    ModelState.AddModelError("", "Group creation failed");
                }
            }

            return View(model);
        }

        //
        // GET: /Group/Edit    
        public ActionResult Edit(int id)
        {
            var group = groupService.GetDetails(id);

            if (group != null)
            {
                var model = new GroupEditViewModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description
                };

                return View(model);
            }

            else return RedirectToAction("Index", "Group");
        }

        //
        // POST: /Group/Edit
        [HttpPost]
        public ActionResult Edit(GroupEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = groupService.Edit(new GroupModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                });

                if (result)
                {
                    return RedirectToAction("Index", "Group");
                }
                else
                {
                    ModelState.AddModelError("", "Group creation failed");
                }
            }

            return View(model);
        }


        //
        // GET: /Group/Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var group = groupService.GetDetails(id);

            if (group != null)
            {
                var model = new GroupDetailsViewModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    MembersCount = group.Members.Count(),
                    StoryesCount = group.Stories.Count(),
                    IsJoined = group.Members.Any(l => l.Id == CurrentUserId),
                    Members = group.Members.Select(m => new UserViewModel
                    {
                        Id = m.Id,
                        Name = m.Name
                    }),
                    Stories = group.Stories.Select(m => new StoryViewModel
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        Description = m.Description,
                        PostedOn = m.PostedOn,
                        IsEditable = (storyService.GetDetails(m.Id).CreatorId == CurrentUserId)
                    })
                };


                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Group was not found");
            }

            return RedirectToAction("Index", "Group");
        }

        [HttpGet]
        public ActionResult Join(int id)
        {
            var result = groupService.AddMember(id, CurrentUserId);

            if (result)
            {
                return RedirectToAction("Index", "Group");
            }

            return RedirectToAction("Details", "Group", new { id = id });
        }



        [HttpGet]
        public ActionResult Leave(int id)
        {
            var result = groupService.RemoveMember(id, CurrentUserId);

            if (result)
            {
                return RedirectToAction("Index", "Group");
            }

            return RedirectToAction("Details", "Group", new { id = id });
        }
    }
}