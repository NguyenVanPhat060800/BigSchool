using BigSchool.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        public Course ObjCourse { get; private set; }

        // GET: Courses
        public ActionResult Create()
        {
            BigSchoolConText contex = new BigSchoolConText();
            Course ObjCourse = new Course();
            ObjCourse.ListCategory = contex.Categories.ToList();


            return View(ObjCourse);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objCourse)
        {
            BigSchoolConText contex = new BigSchoolConText();
            ModelState.Remove("LecturerId");
            if (!ModelState.IsValid)
            {
                objCourse.ListCategory = contex.Categories.ToList();
                return View("Create", objCourse);
            }

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objCourse.LecturerId = user.Id;

            contex.Courses.Add(ObjCourse);
            contex.SaveChanges();





            return RedirectToAction("index", "Home");
        }
    }
}