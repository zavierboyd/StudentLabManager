using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace StudentLabManager.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string UserName = HttpContext.User.Claims.Where(user => user.Type == "UserName").First().Value;
                ActiveDirectory User = new ActiveDirectory(UserName);
                string[] ClassGroup = User.GetGroup(UserName);
                ViewBag.ClassList = ClassGroup;
                return View();
            } else
            {
                return View("_InvalidationPage");
            }
        }
        [HttpPost]
        public ActionResult AddTestSchedule(string ClassName)
        {
            return Redirect("ClassDetails/?class=" + ClassName + "&testmanagement=true");
        }
        [HttpPost]
        public ActionResult ClassChoose(string ClassName)
        {
            return Redirect("ClassDetails/?class=" + ClassName);
        }
        public IActionResult ClassDetails()
        {
            string url = HttpContext.Request.Query["class"];
            string testmanagement = HttpContext.Request.Query["testmanagement"];
            ViewBag.URL = url;
            ViewBag.Test = testmanagement;
            return View();
        }
    }
}
