using Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class ArticleDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ArticleTag> ArticleTags { get; set; }



    public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ArticleTag>().HasKey(at => new { at.ArticleId, at.TagId });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticleDbContext).Assembly);
    }

}
