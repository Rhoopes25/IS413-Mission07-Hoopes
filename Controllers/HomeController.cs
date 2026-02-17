using Microsoft.AspNetCore.Mvc;
using Mission06_Hoopes.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
}