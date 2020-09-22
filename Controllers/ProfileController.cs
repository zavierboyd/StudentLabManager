using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentLabManager;

namespace StudentLabManager.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string UserName = HttpContext.User.Claims.Where(user => user.Type == "UserName").First().Value;
                ActiveDirectory User = new ActiveDirectory(UserName);
                ViewBag.DisplayName = User.displayName;
                ViewBag.Role = User.role;
                return View();
            }
            else
            {
                return View("_InvalidationPage");
            }
        }

        [HttpPost]
        public ActionResult ChangeViewToPassword()
        {
            return View("Password");
        }

        public ActionResult ChangeViewToStudentPassword()
        {
            return View("StudentPassword");
        }

        public ActionResult PasswordChange (string oldPassword,string newPassword)
        {
            string UserName = HttpContext.User.Claims.Where(user => user.Type == "UserName").First().Value;
            ActiveDirectory User = new ActiveDirectory(UserName);
            if (User.ChangeOwnPassword(oldPassword, newPassword)){
                ViewBag.PasswordMessage = "Ture";
                return View();
            } else
            {
                ViewBag.PasswordMessage = "False";
                return View();
            }
        }

        public ActionResult StudentPassword(string studentAccount,string newPassword)
        {
            string UserName = HttpContext.User.Claims.Where(user => user.Type == "UserName").First().Value;
            ActiveDirectory User = new ActiveDirectory(UserName);
            if (User.ResetPassword(studentAccount, newPassword))
            {
                ViewBag.PasswordMessage = "Ture";
                return View();
            }
            else
            {
                ViewBag.PasswordMessage = "False";
                return View();
            }
        }
    }
}
