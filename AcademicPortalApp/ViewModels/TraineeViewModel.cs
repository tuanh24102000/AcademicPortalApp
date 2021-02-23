using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AcademicPortalApp.Models
{

    public class TraineeViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string TraineeName { get; set; }
        public string Age { get; set; }
        public string DateOfBirth { get; set; }
        public int TOEICScore { get; set; }
        public string ExperienceDetails { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public ProgrammingLanguages ProgrammingLanguage { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public IEnumerable<ProgrammingLanguages> ProgrammingLanguages { get; set; }
        public string Id { get; set; }
    }


}