using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicPortalApp.Models
{
    public class TrainerCourses
    {
        public int Id { get; set; }
        public Courses Course { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
    }
}