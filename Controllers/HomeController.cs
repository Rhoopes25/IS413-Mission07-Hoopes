using Microsoft.AspNetCore.Mvc;
using Mission06_Hoopes.Models;

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

        return View(movie);
    }
}