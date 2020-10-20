using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            //Claim the context of Principal by Domain info and User info
            PrincipalContext ser = new PrincipalContext(ContextType.Domain, "uict.nz", "DC=uict,DC=nz", txtuser, pwpw);
            
            //Check user's password
            Boolean isValidUser = ser.ValidateCredentials(txtuser, pwpw);

            if (isValidUser) {
                //store user's information into Cookies:Username,Role
                ActiveDirectory User = new ActiveDirectory(txtuser,pwpw, ser);
                ViewBag.Id = User.displayName;
                var claims = new[] { new Claim("UserName", txtuser), new Claim("Role", User.role) };
                ViewBag.Role = User.role;
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                Task.Run(async () =>
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                }).Wait();

                return View("Confirmation");
            } else
            {
                ViewBag.LoginError = true;
                return View("Index");
            }


            
        }


        public IActionResult Confirmation()
        {
           


            return View();
        }

    }
    

}
