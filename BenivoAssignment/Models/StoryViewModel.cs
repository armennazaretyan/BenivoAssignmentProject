using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BenivoAssignment.Models
{
    public class StoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public bool IsEditable { get; set; }
    }


    public class StoryGroupedViewModel
    {
        public IEnumerable<GroupViewModel> Groups { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }
    }

    public class StoryDetailsViewModel : StoryViewModel
    {
        //public bool IsEditable { get; set; }
        public UserViewModel Creator { get; set; }
        public GroupViewModel[] Groups { get; set; }
    }

    public class StoryCreateViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} lenght must be between {2} and {1}", MinimumLength = 4)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "You must select at least one group")]
        [Display(Name = "Select Groups")]
        public int[] GroupIds { get; set; }

        public IEnumerable<SelectListItem> GroupListItems { get; set; }
    }

    public class StoryEditViewModel : StoryCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}