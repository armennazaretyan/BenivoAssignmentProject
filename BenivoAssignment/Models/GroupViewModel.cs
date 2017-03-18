using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BenivoAssignment.Models
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsJoined { get; set; }
        public int MembersCount { get; set; }
        public int StoryesCount { get; set; }
    }

    public class GroupDetailsViewModel : GroupViewModel
    {
        public IEnumerable<UserViewModel> Members { get; set; }
        public IEnumerable<StoryViewModel> Stories { get; set; }
    }

    public class GroupCreateViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} lenght must be between {2} and {1}", MinimumLength = 4)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class GroupEditViewModel : GroupCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }

}