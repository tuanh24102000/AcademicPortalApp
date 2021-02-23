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
        // Get all Trainer Course by trainerId, included trainer and course 
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult AllCourseOfTrainer(string trainerId)
        {
            var allCoursesOfTrainer = _context.TrainerCourses
                .Where(t => t.TrainerId == trainerId).Include(t => t.Trainer).Include(t => t.Course).ToList();
            return View(allCoursesOfTrainer);
        }
        // find trainer course by id and assign trainerid for redirect to all trainer course then remove trainer course had been found
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult RemoveTrainerFromCourse(int Id)
        {
            var findTrainerCourse = _context.TrainerCourses.SingleOrDefault(t => t.Id == Id);
            var trainerId = findTrainerCourse.TrainerId;
            _context.TrainerCourses.Remove(findTrainerCourse);
            _context.SaveChanges();
            return RedirectToAction("AllCourseOfTrainer", "TrainerRelated", new { trainerId = trainerId });
        }
        //GET: Staff/Assign Course to trainer: return :select trainer and course view model to view     
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult AssignCourseToTrainer()
        {
            var viewModel = new TrainerCoursesViewModel()
            {
                Trainers = _context.Users.OfType<Trainer>().ToList(),
                Courses = _context.Courses.ToList()
            };

            return View(viewModel);
        }
        //POST: Staff/ receive trainerid and courseid from viewmodel to create new trainer course and redirect to all trainer course page 
        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult AssignCourseToTrainer(TrainerCoursesViewModel model)
        {
            var IfCourseExist = _context.TrainerCourses.SingleOrDefault(t => t.TrainerId == model.TrainerId && t.CourseId == model.CourseId);
            if (IfCourseExist != null)
            {
                var viewModel = new TrainerCoursesViewModel()
                {
                    Trainers = _context.Users.OfType<Trainer>().ToList(),
                    Courses = _context.Courses.ToList()

                };
                ViewBag.message = "This Course had been assigned to this trainer";
                return View(viewModel);
            }
            else
            {
                var trainerCourses = new TrainerCourses()
                {
                    TrainerId = model.TrainerId,
                    CourseId = model.CourseId
                };
                _context.TrainerCourses.Add(trainerCourses);
                _context.SaveChanges();

                return RedirectToAction("AllCourseOfTrainer", "TrainerRelated", new { trainerId = model.TrainerId });
            }
        }
        //GET: Staff/ find trainer course by id and trainer id and create a new view model of trainer course to return trainer course, list of course and trainer id
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult ReassignedTrainerToCourse(int Id)
        {
            var trainerCourse = _context.TrainerCourses.SingleOrDefault(t => t.Id == Id);
            var trainerId = trainerCourse.TrainerId;
            TrainerCoursesViewModel model = new TrainerCoursesViewModel
            {
                TrainerCourse = trainerCourse,
                Courses = _context.Courses.ToList(),
                TrainerId = trainerId
            };

            return View(model);
        }
        //POST: Staff/ find trainer course by trainer course id and change courseid that receive from view model
        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult ReassignedTrainerToCourse(TrainerCoursesViewModel model)
        {
            var trainerCourse = _context.TrainerCourses.SingleOrDefault(t => t.Id == model.TrainerCourse.Id);
            trainerCourse.CourseId = model.TrainerCourse.CourseId;
            _context.SaveChanges();

           return RedirectToAction("AllCourseOfTrainer", "TrainerRelated", new { trainerId = model.TrainerId });
        }
    }
}