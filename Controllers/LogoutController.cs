using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace StudentLabManager.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {

            Task.Run(async () =>
            {
                await HttpContext.SignOutAsync();
            }).Wait();
            return RedirectToAction("Index", "Home");
        }
    }
}
