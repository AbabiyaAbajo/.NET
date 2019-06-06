using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab4b.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab4b.Controllers
{
    public class HomeController : Controller
    {
        private MoviesContext _movieContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public HomeController(MoviesContext context)
        {
            _movieContext = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            /*
            USE [master] 
            GO 
            CREATE DATABASE [Lab4a]
            GO 

            USE [Lab4a] 
            GO 
            CREATE TABLE Movies( 
            [MovieId] [int] NOT NULL PRIMARY KEY IDENTITY(1,1), 
            [Title] [nvarchar](1000) NOT NULL, 
            [SubTitle] [nvarchar](1000) NOT NULL, 
            [Description] [nvarchar](1000) NOT NULL, 
            [Year] [datetime] NOT NULL, 
            [Rating] [int] NOT NULL) 
            GO               
            )
            */

            return View(_movieContext.Movies.ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult AddMovie()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateMovie(Movie movie)
        {
            _movieContext.Movies.Add(movie);
            _movieContext.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult EditMovie(int id)
        {
            var movieToUpdate = (from c in _movieContext.Movies where c.MovieId == id select c).FirstOrDefault();

            return View(movieToUpdate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ModifyMovie(Movie movie)
        {
            var id = Convert.ToInt32(Request.Form["MovieId"]);

            var movieToUpdate = (from c in _movieContext.Movies where c.MovieId == id select c).FirstOrDefault();
            movieToUpdate.Title = movie.Title;
            movieToUpdate.SubTitle = movie.SubTitle;
            movieToUpdate.Description = movie.Description;
            movieToUpdate.Year = movie.Year;
            movieToUpdate.Rating = movie.Rating;

            _movieContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteMovie(int id)
        {
            var movieToDelete = (from c in _movieContext.Movies where c.MovieId == id select c).FirstOrDefault();
            _movieContext.Movies.Remove(movieToDelete);

            _movieContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
