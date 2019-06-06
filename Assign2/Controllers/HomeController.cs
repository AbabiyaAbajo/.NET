using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using assignment2Net.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.EntityFrameworkCore;

namespace assignment2Net.Controllers
{
    public class HomeController : Controller
    {
        private assign2DBContext _assignContext;

        public HomeController(assign2DBContext context)
        {
            _assignContext = context;
        }

        public IActionResult Index()
        {
            //var pic = (from p in _assignContext.)
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

        public IActionResult EditProfile(int id)
        {
            var theid = id;
            var profileToUpdate = (from b in _assignContext.Users where b.UserId == id select b).FirstOrDefault();
            return View(profileToUpdate);
        }

        [HttpPost]
        public IActionResult EditProfile(Users post)
        {
            var id = Convert.ToInt32(Request.Form["UserId"]);
            var profileToUpdate = (from b in _assignContext.Users where b.UserId == id select b).FirstOrDefault();
            profileToUpdate.FirstName = post.FirstName;
            profileToUpdate.LastName = post.LastName;
            profileToUpdate.Address = post.Address;
            profileToUpdate.City = post.City;
            profileToUpdate.Password = post.Password;
            profileToUpdate.PostalCode = post.PostalCode;
            profileToUpdate.EmailAddress = post.EmailAddress;
            profileToUpdate.DateOfBirth = post.DateOfBirth;
            profileToUpdate.Country = post.Country;
            //_assignContext.Entry(postToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            HttpContext.Session.SetString("Name", profileToUpdate.FirstName + " " + profileToUpdate.LastName);

            _assignContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {

            return View();
        }
        public IActionResult AddBlogPost()
        {
            return View();
        }

        public IActionResult ViewBadWords()
        {
            return View(_assignContext.BadWords.ToList());
        }

        public IActionResult AddBadWord(BadWords w)
        {
            _assignContext.BadWords.Add(w);
            _assignContext.SaveChanges();

            return RedirectToAction("ViewBadWords");
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

                if (info != null)
                {
                    ViewBag.Message = user.EmailAddress + " already exists. Please login instead.";
                }
                else
                {
                    _assignContext.Users.Add(user);
                    _assignContext.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Login");
                    //ViewBag.Message = user.FirstName + " " + "successfully registered.";
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
                    HttpContext.Session.SetString("Name", result.FirstName + " " + result.LastName);
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
                //BlogPosts blog = new BlogPosts();
                //blog.BlogPostId = blogPost.BlogPostId;
                //blog.Content = blogPost.Content;
                //blog.Posted = blogPost.Posted;
                //blog.Title = blogPost.Title;

                var commentHash = (from c in _assignContext.Comments where c.BlogPostId == id select c).ToList();
                var baddie = (from b in _assignContext.BadWords select b).ToList();
                
                //BadWords b = new BadWords();
                foreach (Comments com in commentHash)
                {  
                    foreach (BadWords b in baddie)
                    {
                        com.Content = com.Content.ToLower().Replace(b.Word.ToLower(), "****");
                    }
                    blogPost.Comments.Add(com);
                }
                var photoHash = (from c in _assignContext.Photos where c.BlogPostId == id select c).ToList();

                foreach (Photos com in photoHash)
                {
                    blogPost.Photos.Add(com);
                }
                //blogPost.Photos = (from p in _assignContext.Photos where p.BlogPostId == blogPost.BlogPostId select)
                blogPost.User = (from user in _assignContext.Users where user.UserId == blogPost.UserId select user).FirstOrDefault();


                //var view = _assignContext.Photos.Include(b => b.BlogPost).AsNoTracking(); //from o in _assignContext.BlogPosts join o2 in _assignContext.Photos on o.BlogPostId equals o2.BlogPostId where o.BlogPostId.Equals(o2.BlogPostId)
                return View(blogPost);
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


            return RedirectToAction("DisplayFullBlogPost");
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

            //if(post.Content.Length < 397)
            //{
            //    post.ShortDescription = post.Content + "...";
            //}
            //else
            //{
            //    post.ShortDescription = post.Content.Substring(0, 397) + "...";
            //}

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
            //var theid = id;
            var postToUpdate = (from b in _assignContext.BlogPosts where b.BlogPostId == id select b).FirstOrDefault();

            var photoHash = (from c in _assignContext.Photos where c.BlogPostId == id select c).ToList();
            foreach (Photos com in photoHash)
            {
                postToUpdate.Photos.Add(com);
            }

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
            postToUpdate.ShortDescription = post.ShortDescription;

            //if (post.Content.Length < 397)
            //{
            //    postToUpdate.ShortDescription = post.Content + "...";
            //}
            //else
            //{
            //    postToUpdate.ShortDescription = post.Content.Substring(0, 397) + "...";
            //}
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

        public IActionResult DeleteBadWord(int id)
        {
            var bW = (from b in _assignContext.BadWords where b.BadWordId == id select b).FirstOrDefault();
            _assignContext.BadWords.Remove(bW);
            _assignContext.SaveChanges();
            return RedirectToAction("ViewBadWords");
        }

        public IActionResult Delete(int id)
        {
            var im = (from i in _assignContext.Photos where id == i.PhotoId select i).FirstOrDefault();

            _assignContext.Photos.Remove(im);
            _assignContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileNow(ICollection<IFormFile> files)
        {

            // get your storage accounts connection string
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=abajobir2assign;AccountKey=mofWYC2enRo7QgLEny1RyoypgzEXNxJZHylHVXOSulTmwCkp9qGVOKLzRb3HRBNLnBbwk2qTtqO1I25tGbZ8jA==;");

            // create an instance of the blob client
            var blobClient = storageAccount.CreateCloudBlobClient();

            // create a container to hold your blob (binary large object.. or something like that)
            // naming conventions for the curious https://msdn.microsoft.com/en-us/library/dd135715.aspx
            var container = blobClient.GetContainerReference("abbiphotostorage");
            await container.CreateIfNotExistsAsync();

            // set the permissions of the container to 'blob' to make them public
            var permissions = new BlobContainerPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
            await container.SetPermissionsAsync(permissions);

            // for each file that may have been sent to the server from the client
            foreach (var file in files)
            {
                try
                {
                    // create the blob to hold the data
                    var blockBlob = container.GetBlockBlobReference(file.FileName);
                    if (await blockBlob.ExistsAsync())
                        await blockBlob.DeleteAsync();

                    using (var memoryStream = new MemoryStream())
                    {
                        // copy the file data into memory
                        await file.CopyToAsync(memoryStream);

                        // navigate back to the beginning of the memory stream
                        memoryStream.Position = 0;

                        // send the file to the cloud
                        await blockBlob.UploadFromStreamAsync(memoryStream);
                    }

                    // add the photo to the database if it uploaded successfully
                    var photo = new Photos();
                    photo.Url = blockBlob.Uri.AbsoluteUri;
                    photo.Filename = file.FileName;
                    photo.BlogPostId = Convert.ToInt32(Request.Form["BlogPostId"]);

                    _assignContext.Photos.Add(photo);
                    _assignContext.SaveChanges();
                }
                catch
                {

                }
            }

            return RedirectToAction("EditBlogPost");
        }

    }
}