using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Booking.Models;
using Newtonsoft.Json;

namespace Movie_Booking.Controllers
{
    public class MoviesController : Controller
    {
        private readonly Movie_BookingContext _context;
        private readonly WebClient webClient = new WebClient();

        public MoviesController(Movie_BookingContext context)
        {
            _context = context;
        }

        // GET: Movies
        public IActionResult Index() {

            byte[] myDataBuffer = webClient.DownloadData("https://simonsmoviebooking.azurewebsites.net/api/movie");
            string downloadedString = Encoding.ASCII.GetString(myDataBuffer);

            var movie = JsonConvert.DeserializeObject<List<Movie>>(downloadedString);

            return View(movie);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie) {
            string Json = JsonConvert.SerializeObject(movie);
            byte[] bytes = Encoding.ASCII.GetBytes(Json);
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.UploadData("http://simonsmoviebooking.azurewebsites.net/api/movie", "POST", bytes);

            return this.RedirectToAction("Index", "Movies");
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Numseats")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            byte[] myDataBuffer = webClient.DownloadData("https://simonsmoviebooking.azurewebsites.net/api/movie/" + id);
            string downloadedString = Encoding.ASCII.GetString(myDataBuffer);

            var movie = JsonConvert.DeserializeObject<Movie>(downloadedString);

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            webClient.UploadData("http://simonsmoviebooking.azurewebsites.net/api/movie/" + id, "DELETE", new byte[10]);

            return this.RedirectToAction("Index", "Movies");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }

        public IActionResult Booking(int? id) {
            if (id == null) {
                return NotFound();
            }

            webClient.UploadData("http://simonsmoviebooking.azurewebsites.net/api/movie/BookMovie/" + id, "PUT", new byte[10]);

            return this.RedirectToAction("Index", "Movies");
        }
    }
}
