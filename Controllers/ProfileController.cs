﻿using System;
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
            if (HttpContext.User.Identity.IsAuthenticated) //User must be logged.
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
            //Claim a ActiveDirectory object:User for further code.
            string UserName = HttpContext.User.Claims.Where(user => user.Type == "UserName").First().Value;
            ActiveDirectory User = new ActiveDirectory(UserName);

            //invoke ChangeOwnPassword methed, it will return if the operation is success.
            if (User.ChangeOwnPassword(oldPassword, newPassword))
            {
                ViewBag.PasswordState = true;
                ViewBag.PasswordMessage = "Password Changed!";
                return View("Password");
            } else
            {
                ViewBag.PasswordState = false;
                ViewBag.PasswordMessage = "Password Change Failed";
                return View("Password");
            }
        }

        public ActionResult StudentPassword(string studentAccount,string newPassword,string adminPassword)
        {
            //Claim a ActiveDirectory object:User for further code.
            string UserName = HttpContext.User.Claims.Where(user => user.Type == "UserName").First().Value;
            ActiveDirectory User = new ActiveDirectory(UserName);

            ////invoke ResetPassword methed, it will return if the operation is success.
            if (User.ResetPassword(studentAccount, newPassword, adminPassword))
            {
                ViewBag.PasswordState = true;
                ViewBag.PasswordMessage = "Password Changed!";
                return View("StudentPassword");
            }
            else
            {
                ViewBag.PasswordState = false;
                ViewBag.PasswordMessage = "Password Change Failed";
                return View("StudentPassword");
            }
        }
    }
}
