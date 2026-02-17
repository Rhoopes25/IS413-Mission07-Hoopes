using Microsoft.AspNetCore.Mvc;
using Mission06_Hoopes.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mission06_Hoopes.Controllers;

public class HomeController : Controller
{
    private readonly MovieCollectionContext _context;

    public HomeController(MovieCollectionContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View();

    public IActionResult GetToKnowJoel() => View();

    [HttpGet]
    public IActionResult AddMovie()
    {
        PopulateCategoriesDropdown();
        return View(new Movie());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddMovie(Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return View("Confirmation", movie);
        }

        PopulateCategoriesDropdown(movie.CategoryId); //repopulate the dropdown
        return View(movie);
    }
    private void PopulateCategoriesDropdown(int? selectedCategoryId = null)
    {
        var categories = _context.Categories
            .OrderBy(c => c.CategoryName)
            .Select(c => new { c.CategoryId, c.CategoryName })
            .ToList();

        ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", selectedCategoryId);
    }
    public IActionResult Collection() // look at movies in DB
    {
        var movies = _context.Movies
            .Include(m => m.Category)   // display the category name
            .OrderBy(m => m.Title)
            .ToList();

        return View(movies);
    }
    
    // GET: show the edit form pre-filled
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie == null) return NotFound();

        // fill the dropdown and preselect this movie's category
        PopulateCategoriesDropdown(movie.CategoryId);
        return View(movie);   // returns Views/Home/Edit.cshtml with the movie model
    }
    // POST: user submitted the edit form — save changes
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Movie movie)
    {
        // tiny safety: URL id must match posted model id
        if (id != movie.MovieId) return BadRequest();

        // check validations (Title required, Year >=1888, Category selected, etc.)
        if (ModelState.IsValid)
        {
            // update the DB and go back to the Collection page
            _context.Update(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Collection));
        }

        // if we got here, validation failed — re-render form and repopulate dropdown
        PopulateCategoriesDropdown(movie.CategoryId);
        return View(movie);
    }
    
    // GET: show confirmation page
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var movie = _context.Movies.Include(m => m.Category).FirstOrDefault(m => m.MovieId == id);
        if (movie == null) return NotFound();
        return View(movie);   // returns Views/Home/Delete.cshtml
    }
    
    // POST: confirmed deletion
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie != null)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Collection));
    }

    
    
    
}