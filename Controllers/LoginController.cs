using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentLabManager.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult form1(string txtuser, string pwpw)
        {
            ViewBag.Id = txtuser;
            ViewBag.Name = pwpw;
            Console.WriteLine(txtuser);
           

            return View("Confirmation");
            
        }


        public IActionResult Confirmation()
        {
           


            return View();
        }
    }


}
