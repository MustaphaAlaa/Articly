using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Entities.ViewsModel.Articles;
using Entities.ViewsModel.Tags;
namespace  DataAccessLayer;

public   class ArticleDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public DbSet<ArticlesTags> ArticlesTags { get; set; }

    public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


    }

}
