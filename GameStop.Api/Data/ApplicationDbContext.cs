using GameStop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options ) : base(options) { }

    public DbSet<VideoGame> VideoGames { get; set; }
}