using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AcademicPortalApp.Models
{
    public class CourseAndCategoryViewModel
    {
        public Courses Course { get; set; }
        public Categories Category { get; set; }
        public IEnumerable<Categories> Categories { get; set; }

    }
}