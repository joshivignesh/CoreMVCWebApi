using HexaBlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HexaBlogAPI.Infrastructure
{
    public class BlogsContext : DbContext
    { 

    public BlogsContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Blog> blogs { get; set; }
    }
}
