using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private ApplicationContext contextDatabase;
        private List<string> AllText;
        public HomeController(ApplicationContext contextDatabase)
        {
            AllText = new List<string>();
            this.contextDatabase = contextDatabase;
        }


        public IActionResult Index()
        {

            AllText.Add("Hello");
            AllText.Add("World");
            return View(AllText);
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

        public JsonResult AddPost(string message)
        {

            return Json(new { data = new List<string> { "lol", "kek", message } });
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
            // здесь дето ошибка
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
