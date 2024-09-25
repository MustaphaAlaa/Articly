using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articly.Entites.Cong;

public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
{
    public void Configure(EntityTypeBuilder<ArticleTag> builder)
    {
        builder.HasKey(articleTag => new { articleTag.TagId, articleTag.ArticleId });


        builder.HasOne(articleTag => articleTag.Article)
               .WithMany(article => article.ArticleTag)
               .HasForeignKey(articleTag => articleTag.ArticleId);

        builder.HasOne(articleTag => articleTag.Tag)
               .WithMany(tag => tag.ArticleTag)
               .HasForeignKey(articleTag => articleTag.TagId);



        builder.ToTable("ArticleTag");


    }
}
