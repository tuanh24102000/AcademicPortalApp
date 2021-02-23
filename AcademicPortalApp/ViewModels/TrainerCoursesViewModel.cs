using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AcademicPortalApp.Models
{
    public class TrainerCoursesViewModel
    {
        public string TrainerId { get; set; }
        public IEnumerable<Trainer> Trainers { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<Courses> Courses { get; set; }
        public TrainerCourses TrainerCourse { get; set; }
    }
}