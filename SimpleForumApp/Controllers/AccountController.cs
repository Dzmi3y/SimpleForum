using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using SimpleForumApp.Models;
using SimpleForumApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;



namespace SimpleForumApp.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext contextDatabase;

        public AccountController(ApplicationContext contextDatabase)
        {
            this.contextDatabase = contextDatabase;
            DatabaseInitialize();
        }

        private void DatabaseInitialize()
        {   
            if (!contextDatabase.Roles.Any())
            {
                
                string adminRoleName = "Admin";
                string userRoleName = "User";

                string adminEmail = "Admin@gmail.com";
                string adminPass = "123456";

                Role adminRole = new Role { Name = adminRoleName };
                Role userRole = new Role { Name = userRoleName };

                contextDatabase.Roles.Add(adminRole);
                contextDatabase.Roles.Add(userRole);

                contextDatabase.Users.Add(new User { Email = adminEmail, Password = adminPass, Role = adminRole });
                contextDatabase.SaveChanges();
            }
        }

        [Authorize(Roles = "Admin, User")] 
        public IActionResult Index()
        {
            
            if (User.IsInRole("User"))
            {
                return Content("You user");
            }
            else
            {
                return Content("You admin");
            }
        }

        


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await contextDatabase.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (currentUser == null)
                {
                    currentUser = new User { Email = model.Email, Password = model.Password };
                    Role userRole = await contextDatabase.Roles.FirstOrDefaultAsync(r => r.Name == "User");

                    if (userRole != null)
                    {
                        currentUser.Role = userRole;

                    }

                    contextDatabase.Users.Add(currentUser);
                    await contextDatabase.SaveChangesAsync();
                    await Autenticate(currentUser);
                   
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }


            }
            return View(model);

        }


        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User currenUser = await contextDatabase.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && model.Password == u.Password);

                if (currenUser != null)
                {
                    await Autenticate(currenUser);
                    //return RedirectToAction("Index","Home");

                    return RedirectToAction("Index","Account");
                   
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");
        }

        private async Task Autenticate(User currentUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,currentUser.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,currentUser?.Role.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            ViewData.Add("role",currentUser.Role.Name);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

    }
}
