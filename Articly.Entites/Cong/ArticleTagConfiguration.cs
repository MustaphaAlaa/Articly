using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articly.Entites.Cong;

public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
{
    public void Configure(EntityTypeBuilder<ArticleTag> builder)
    {
        builder.HasKey(article => article.Id);

        //Relation Many-To-Many

        builder.Property(a => a.UpdatedAt)
           .HasColumnType("datetime2")
           .IsRequired(true);

        builder.Property(a => a.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired(true);


        builder.ToTable("ArticleTag");


    }
}
