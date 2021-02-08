using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicPortalApp.Models;

namespace AcademicPortalApp.Controllers
{
    public class TrainerRelatedController : Controller
    {
        private ApplicationDbContext _context;

        public TrainerRelatedController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: TrainerRelated
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult AllCourseOfTrainer(string trainerId)
        {
            var allCoursesOfTrainer = _context.TrainerCourses
                .Where(t => t.TrainerId == trainerId)
                .Select(t => t.Course)
                .Include(t => t.Category).ToList();

            List<TrainerCoursesViewModel> courseWithTrainerId = new List<TrainerCoursesViewModel>();

            foreach (var item in allCoursesOfTrainer)
            {
                courseWithTrainerId.Add(new TrainerCoursesViewModel()
                {
                    TrainerId = trainerId,
                    Course = item
                });
            }

            return View(courseWithTrainerId);
        }
    }
}