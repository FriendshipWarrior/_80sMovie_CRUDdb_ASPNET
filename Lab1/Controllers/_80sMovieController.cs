using Lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Lab1.Controllers
{
    public class _80sMovieController : Controller
    {
        //
        // GET: /_80sMovie/
        public ActionResult Index()
        {
            List<_80sMovieModel> list = DatabaseQueries.ReadAllMovies();
            return View(list);
        }

        //
        // GET: /_80sMovie/Details/5
        public ActionResult Details(int id)
        {
            _80sMovieModel movie = DatabaseQueries.ReadMovie(id);
            return View(movie);
        }

        //
        // GET: /_80sMovie/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /_80sMovie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                _80sMovieModel movie = new _80sMovieModel()
                {
                    Title = collection["Title"],
                    Genre = collection["Genre"],
                    Rating = float.Parse(collection["Rating"]),
                    ReleaseDate = DateTime.Parse(collection["ReleaseDate"]),
                    Seen = collection["Seen"]!="false"
                };

                DatabaseQueries.CreateMovie(movie);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.InternalServerError,
                    "Error commiting infor to database: " + ex.Message);
            }
        }

        //
        // GET: /_80sMovie/Edit/5
        public ActionResult Edit(int id)
        {
            _80sMovieModel movie = DatabaseQueries.ReadMovie(id);
            return View(movie);
        }

        //
        // POST: /_80sMovie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                _80sMovieModel movie = new _80sMovieModel()
                {
                    Id = id,
                    Title = collection["Title"],
                    Genre = collection["Genre"],
                    Rating = float.Parse(collection["Rating"]),
                    ReleaseDate = DateTime.Parse(collection["ReleaseDate"]),
                    Seen = collection["Seen"]!="false"
                };
                DatabaseQueries.UpdateMovie(movie);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.InternalServerError,
                    "Error commiting infor to database: " + ex.Message);   
            }
        }

        //
        // GET: /_80sMovie/Delete/5
        public ActionResult Delete(int id)
        {
            _80sMovieModel movie = DatabaseQueries.ReadMovie(id);
            return View(movie);
        }

        //
        // POST: /_80sMovie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                DatabaseQueries.DeleteMovie(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.InternalServerError,
                    "Error commiting infor to database: " + ex.Message);
            }
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
