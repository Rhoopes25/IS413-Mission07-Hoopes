using Microsoft.EntityFrameworkCore;

namespace Mission06_Hoopes.Models;

public class MovieCollectionContext : DbContext
{
    public MovieCollectionContext(DbContextOptions<MovieCollectionContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
}