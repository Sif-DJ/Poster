using Microsoft.EntityFrameworkCore;
using Poster.Core.Models;

namespace Poster.Infrastructure;

public class PosterContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public string DbPath { get; }
    
    public PosterContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Combine(path, "Poster.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
    }
}