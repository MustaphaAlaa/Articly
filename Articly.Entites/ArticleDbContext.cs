using Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class ArticleDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<ArticleTag> ArticleTag { get; set; }

    public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticleDbContext).Assembly);

    }

}
