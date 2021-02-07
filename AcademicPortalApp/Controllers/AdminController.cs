using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using Microsoft.Owin.Security;
using AcademicPortalApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
namespace AcademicPortalApp.Controllers
{
    public class AdminController : Controller
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

        public AdminController()
            
        {
            _context = new ApplicationDbContext();
            _user = new ApplicationUser();
            _manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>( new ApplicationDbContext()));
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        //get all trainer by Discriminator Trainer
        public ActionResult AllTrainer()
        {
            var allTrainer = _context.Users.OfType<Trainer>().Include(t => t.Type).ToList();

            List<TrainerViewModel> trainerInfo = new List<TrainerViewModel>();

            foreach(var trainer in allTrainer)
            {

                trainerInfo.Add(new TrainerViewModel()
                {
                    Trainer = trainer,
                    UserName = trainer.UserName
                });
            }

            return View(trainerInfo);
        }
        //get all staff
        public ActionResult AllStaff()
        {
            return View();
        }
        //
        // GET: /Admin/Create new staff
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateNewStaff()
        {
            return View();
        }

        //
        // POST: /Admin/Create new staff account
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateNewStaff(CreateNewTrainerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        // GET: /Admin/Create new trainer account
        public ActionResult CreateNewTrainer()
        {
            CreateNewTrainerViewModel model = new CreateNewTrainerViewModel() {
                Types = _context.Types.ToList()
            };
            return View(model);
        }

        //
        // POST: /Admin/Create new trainer account
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewTrainer(CreateNewTrainerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Trainer { UserName = model.Email, Email = model.Email, WorkingPlace = model.WorkingPlace, TypeId = model.TypeId, TrainerName= model.TrainerName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


    }


}