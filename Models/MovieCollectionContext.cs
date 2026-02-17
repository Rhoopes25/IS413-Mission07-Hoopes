using Microsoft.EntityFrameworkCore;

namespace Mission06_Hoopes.Models;

public class MovieCollectionContext : DbContext
{
    public MovieCollectionContext(DbContextOptions<MovieCollectionContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Movie> Movies { get; set; }   // should already be here

    
}