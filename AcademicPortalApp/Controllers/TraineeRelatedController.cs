using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicPortalApp.Models;

namespace AcademicPortalApp.Controllers
{
    public class TraineeRelatedController : Controller
    {
        private ApplicationDbContext _context;
        public TraineeRelatedController()
        {
            _context = new ApplicationDbContext();
        }
        // Get all Trainee Course by traineeId, included trainee and course 
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult AllCourseOfTrainee(string traineeId)
        {
            var allCoursesOfTrainee = _context.TraineeCourses
                .Where(t => t.TraineeId == traineeId).Include(t => t.Trainee).Include(t => t.Course).ToList();
            return View(allCoursesOfTrainee);
        }
        //GET: Staff/Assign Course to trainee: return :select trainee and course view model to view 
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult AssignCourseToTrainee()
        {
            var viewModel = new TraineeCourseViewModel()
            {
                Trainees = _context.Users.OfType<Trainee>().ToList(),
                Courses = _context.Courses.ToList()
            };
            return View(viewModel);
        }
        //POST: Staff/ receive traineeid and courseid from viewmodel to create new trainee course and redirect to all trainee course page 
        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult AssignCourseToTrainee(TraineeCourseViewModel model)
        {
            var traineeCourses = new TraineeCourses()
            {
                TraineeId = model.TraineeId,
                CourseId = model.CourseId
            };
            _context.TraineeCourses.Add(traineeCourses);
            _context.SaveChanges();

            return RedirectToAction("AllCourseOfTrainee", "TraineeRelated", new { traineeId = model.TraineeId });
        }
        // find trainee course by id and assign traineeid for redirect to all trainee course then remove trainee course had been found
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult RemoveTraineeFromCourse(int Id)
        {
            var findTraineeCourse = _context.TraineeCourses.SingleOrDefault(t => t.Id == Id);
            var traineeId = findTraineeCourse.TraineeId;
            _context.TraineeCourses.Remove(findTraineeCourse);
            _context.SaveChanges();
            return RedirectToAction("AllCourseOfTrainee", "TraineeRelated", new { traineeId = traineeId });
        }
        //GET: Staff/ find trainee course by id and trainee id then create a new view model of trainer course to return trainee course, list of course and trainee id
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult ReassignedTraineeToCourse(int Id)
        {
            var traineeCourse = _context.TraineeCourses.SingleOrDefault(t => t.Id == Id);
            var traineeId = traineeCourse.TraineeId;
            TraineeCourseViewModel model = new TraineeCourseViewModel
            {
                TraineeCourse = traineeCourse,
                Courses = _context.Courses.ToList(),
                TraineeId = traineeId
            };

            return View(model);
        }
        //POST: Staff/ find trainee course by trainee course id and change courseid that receive from view model
        [HttpPost]
        [Authorize(Roles = "Staff")]
        public ActionResult ReassignedTraineeToCourse(TraineeCourseViewModel model)
        {
            var traineeCourse = _context.TraineeCourses.SingleOrDefault(t => t.Id == model.TraineeCourse.Id);
            traineeCourse.CourseId = model.TraineeCourse.CourseId;
            _context.SaveChanges();

            return RedirectToAction("AllCourseOfTrainee", "TraineeRelated", new { traineeId = model.TraineeId });
        }
    }
}