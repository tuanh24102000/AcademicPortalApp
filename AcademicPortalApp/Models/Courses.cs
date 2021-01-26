﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace AcademicPortalApp.Models
{
    public class Courses
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Course Name must not be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Course Description must not be empty")]
        public string Description { get; set; }
        public Categories Category { get; set; }
        public int CategoryId { get; set; }
    }
}