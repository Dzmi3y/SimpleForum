using System;
using System.Collections.Generic;   
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleForumApp.Models;


namespace SimpleForumApp.Controllers
{
    public class HomeController : Controller
    {
        public IApplicationContext contextDatabase { get; private set; }
        private List<string> AllText;

        
        public HomeController(IApplicationContext contextDatabase)
        {
            AllText = new List<string>();
            this.contextDatabase = contextDatabase;
        }

        
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LoadComments(int idPost)
        {
            IEnumerable<Commentary> commentaries = contextDatabase.Commentaries.Include(c => c.Post).Include(c => c.User).Where(c => c.PostId == idPost);
            return PartialView(commentaries);
        }

        public JsonResult AddComment(string text, int idPost)
        {
            string currentUserEmail = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            User currentUser = contextDatabase.Users.Include(u => u.Posts).FirstOrDefault(u => u.Email == currentUserEmail);

            Commentary NewComment = new Commentary();
            NewComment.Date = DateTime.Now;
            NewComment.Text = text;
            NewComment.User = currentUser;
            NewComment.Post = contextDatabase.Posts.FirstOrDefault(p => p.Id == idPost);
            
            contextDatabase.Commentaries.Add(NewComment);
            contextDatabase.SaveChanges();

            return Json(true);
        }

        public IActionResult GetPosts()
        {
            List<Post> AllPosts = contextDatabase.Posts                                                   
                                                        .Include(p => p.Commentaries)
                                                        .Include(p => p.User)
                                                        .OrderByDescending(p => p.Date).ToList();
            return PartialView(AllPosts);
        }
    }
}
