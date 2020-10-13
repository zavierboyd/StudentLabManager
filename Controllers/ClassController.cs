using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentLabManager.Controllers
{
    public class ClassController : Controller
    {
        private Tuple<ActiveDirectory, string> AuthenticateUser(HttpContext httpContext)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string UserName = HttpContext.User.Claims.Where(user => user.Type == "UserName").First().Value;
                var User = new ActiveDirectory(UserName);
                return new Tuple<ActiveDirectory, string>(User, UserName);
            }
            return null;

        }
        private string[] GetGroups(Tuple<ActiveDirectory, string> tuple)
        {
            string UserName = tuple.Item2;
            ActiveDirectory User = tuple.Item1;
            string[] ClassGroup = User.GetGroup(UserName);
            return ClassGroup;

        }
        public IActionResult Index()
        {
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null)
            {
                ViewBag.ClassList = GetGroups(tuple);
                return View();
            }
            return View("_InvalidationPage");
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
            var tuple = AuthenticateUser(HttpContext);
            if (tuple != null)
            {
                ViewBag.ClassList = GetGroups(tuple);
                string group = HttpContext.Request.Query["class"];
                Predicate<string> test = (string tgroup) => { return tgroup == group; };
                if (Array.Exists<string>(ViewBag.ClassList, test))
                {
                    string testmanagement = HttpContext.Request.Query["testmanagement"];
                    if (testmanagement == "true")
                    {
                        // Do test management view
                    } else
                    {
                        // Do something else view
                    }
                    ViewBag.URL = group;
                    ViewBag.Test = testmanagement;
                    return View();
                }

            }
            return View("_InvalidationPage");
            
        }
    }
}
