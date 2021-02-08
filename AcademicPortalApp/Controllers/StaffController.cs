using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AcademicPortalApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace AcademicPortalApp.Controllers
{
    [Authorize(Roles ="Staff")]
    public class StaffController : Controller
    {
        private ApplicationUserManager _userManager;
        private UserManager<ApplicationUser> _manager;
        private ApplicationSignInManager _signInManager;
        private ApplicationDbContext _context;
        private ApplicationUser _user;
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public StaffController()

        {
            _context = new ApplicationDbContext();
            _user = new ApplicationUser();
            _manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public StaffController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;


        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult AllCourse()
        {
            var allCourse = _context.Courses.ToList();

            List<CourseAndCategoryViewModel> courseWithCate = new List<CourseAndCategoryViewModel>();

            foreach(var course in allCourse)
            {
                courseWithCate.Add(new CourseAndCategoryViewModel()
                {
                    Course = course,
                    Categories = _context.Categories.ToList()
                });

            }
            return View(courseWithCate);
        }

        [Authorize(Roles ="Staff")]
        public ActionResult CreateCourse()
        {
           var selectedcategorylist = new CourseAndCategoryViewModel()
            {
                Categories = _context.Categories.ToList()
            };
            return View(selectedcategorylist);
        }
        [HttpPost]
        [Authorize(Roles = "Staff")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(CourseAndCategoryViewModel model)
        {
           /* var course = new Courses();
            course.Name = model.Course.Name;
            course.CategoryId = model.Course.CategoryId;
            course.Description = model.Course.Description;
*/
            _context.Courses.Add(model.Course);
            _context.SaveChanges();

            return RedirectToAction("AllCourse");
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]
        public ActionResult EditCourse(int Id)
        {
            var modelInfo = new CourseAndCategoryViewModel()
            {
                Course = _context.Courses.SingleOrDefault(t => t.Id == Id),
                Categories = _context.Categories.ToList()
            };
            return View(modelInfo);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public ActionResult EditCourse(CourseAndCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var modelInfo = new CourseAndCategoryViewModel()
                {
                    Course = model.Course,
                    Categories = _context.Categories.ToList()
                };
                return View(modelInfo);
            }       
            var findCourse = _context.Courses.SingleOrDefault(c => c.Id == model.Course.Id);
            if (findCourse == null) 
            {
                return HttpNotFound();
            }
            findCourse.Name = model.Course.Name;
            findCourse.Description = model.Course.Description;
            findCourse.CategoryId = model.Course.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("AllCourse");
        }

        public ActionResult DeleteCourse(int Id)
        {
            var findCourse = _context.Courses.SingleOrDefault(c => c.Id == Id);
            _context.Courses.Remove(findCourse);
            _context.SaveChanges();
            return RedirectToAction("AllCourse");
        }
    }
}