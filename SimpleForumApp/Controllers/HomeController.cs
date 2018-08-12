using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleForumApp.Models;


namespace SimpleForumApp.Controllers
{
    public class HomeController : Controller
    {
        private List<string> AllText;
        public HomeController()
        {
            AllText = new List<string>();
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
            
            return Json(new {data = new List<string> { "lol", "kek", message } });
        }

        

        public IActionResult GetPosts(List<Post> posts)
        {
            //IEnumerable<object> postsList= JsonConvert.DeserializeObject(posts) as IEnumerable<object>;
           // IEnumerable<object> resultPost = postsList.Cast<Post>().ToList();
            return PartialView(posts);
        }
    }
}
