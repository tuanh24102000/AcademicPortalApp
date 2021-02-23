using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AcademicPortalApp.Models
{
    public class TraineeCourseViewModel
    {
        public string TraineeId { get; set; }
        public IEnumerable<Trainee> Trainees { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<Courses> Courses { get; set; }
        public TraineeCourses TraineeCourse { get; set; }
    }

}