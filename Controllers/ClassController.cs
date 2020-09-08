using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                string[] ClassGroup; 
                Array a = User.GetGroup(UserName);
                foreach (object s in a)
                {

                    ClassGroup = new string[] { s.ToString() };
                };
            }
            return View();
        }
    }
}
