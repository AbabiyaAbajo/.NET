using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private abajodbContext _assignContext;

        public HomeController(abajodbContext context)
        {
            _assignContext = context;
        }

        public IActionResult Index()
        {
            return View(_assignContext.BlogPosts.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }
        public IActionResult AddBlogPost()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                var info = _assignContext.Users.Where(u => u.EmailAddress == user.EmailAddress).FirstOrDefault();

                if(info != null)
                {
                ViewBag.Message = user.EmailAddress + " already exists. Please login instead.";
                }
                else
                { 
                _assignContext.Users.Add(user);
                _assignContext.SaveChanges();
                ModelState.Clear();

                ViewBag.Message = user.FirstName + " " + "successfully registered.";
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyLogin()
        {
            //var result = _assignContext.Users.Where(u => u.EmailAddress == login.EmailAddress && u.Password == login.Password).FirstOrDefault();
            if (ModelState.IsValid)
            {

                String emailAddress = Request.Form["EmailAddress"];
                String password = Request.Form["Password"];

                //var result = (from userlist in _assignContext.Users
                //               where userlist.EmailAddress == emailAddress && userlist.Password == password
                //               select new { userlist.UserId, userlist.FirstName, userlist.LastName }).ToList();

                var result = _assignContext.Users.Where(user => user.EmailAddress == emailAddress && user.Password == password).FirstOrDefault();

                //var result = (from u in _assignContext.Users where (u.EmailAddress == emailAddress && u.Password == password) select u).FirstOrDefault();

                if (result != null)
                {
                    //TempData["UserId"] = result.UserId;
                    //TempData["RoleId"] = result.RoleId;
                    //TempData["UserName"] = result.FirstName + " " + result.LastName;
                    
                    ViewData["UserId"] = result.UserId;
                    //ViewData["RoleId"] = result.RoleId;
                    //ViewBag.Message = result.FirstName + " " + result.LastName;
                    //ViewData["UserName"] = result.FirstName + " " + result.LastName;
                    //HttpContext.Session(result.UserId, "UserId");
                    HttpContext.Session.SetInt32("UserId", result.UserId);
                    HttpContext.Session.SetInt32("RoleId", result.RoleId);
                    HttpContext.Session.SetString("Name", result.FirstName);
                    //Session["UserId"] = details.FirstOrDefault().UserId;
                    //Session["FirstName"] = details.FirstOrDefault().FirstName;
                    //Session["LastName"] = details.FirstOrDefault().LastName;
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");

                }

                ViewBag.Message = emailAddress + " email does not exist or the password is incorrect.";
                return View();


            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View("Login");
            }
        } 


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DisplayFullBlogPost(int id)
        {
            var blogPost = (from item in _assignContext.BlogPosts where item.BlogPostId == id select item).FirstOrDefault();
            if (blogPost != null)
            {
                BlogPosts blog = new BlogPosts();
                blog.BlogPostId = blogPost.BlogPostId;
                blog.Content = blogPost.Content;
                blog.Posted = blogPost.Posted;
                blog.Title = blogPost.Title;

                var commentHash = (from c in _assignContext.Comments where c.BlogPostId == id select c).ToList();
                foreach (Comments com in commentHash)
                {
                    //Comments comment = new Comments();                   
                    blog.Comments.Add(com);
                }

                blog.User = (from user in _assignContext.Users where user.UserId == blogPost.UserId select user).FirstOrDefault();
                return View(blog);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult DisplayFullBlogPost()
        {
            Comments comment = new Models.Comments();
            comment.BlogPostId = Convert.ToInt32(Request.Form["BlogPostId"]);
            comment.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            comment.Content = Request.Form["Content"];

            _assignContext.Comments.Add(comment);
            _assignContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //public IActionResult DisplayBlogPost()
        //{
        //    return View("DisplayFullBlogPost");
        //}

        [HttpPost]
        public IActionResult Create(BlogPosts post)
        {
            post.Posted = DateTime.Now;
            //post.UserId = (int)TempData["UserId"];
            post.UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            _assignContext.BlogPosts.Add(post);
            _assignContext.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult EditBlogPost(int id)
        {
            var theid = id;
            var postToUpdate = (from b in _assignContext.BlogPosts where b.BlogPostId == id select b).FirstOrDefault();
            return View(postToUpdate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditBlogPost(BlogPosts post)
        {
            var id = Convert.ToInt32(Request.Form["BlogPostId"]);
            var postToUpdate = (from b in _assignContext.BlogPosts where b.BlogPostId == id select b).FirstOrDefault();
            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            //_assignContext.Entry(postToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _assignContext.SaveChanges();

            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeleteBlogPost(int id)
        {
            var blogToDelete = (from b in _assignContext.BlogPosts where b.BlogPostId == id select b).FirstOrDefault();
            
            var num = (from c in _assignContext.Comments where c.BlogPostId == blogToDelete.BlogPostId select c).ToList();
            foreach (Comments c in num)
            {
                _assignContext.Comments.Remove(c);
                _assignContext.SaveChanges();
            }

            _assignContext.BlogPosts.Remove(blogToDelete);
            //_assignContext.Entry(blogToDelete).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _assignContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
