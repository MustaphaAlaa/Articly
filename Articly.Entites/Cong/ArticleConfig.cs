using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articly.Entites.Cong;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(article => article.ArticleId);






        //Columns
        builder.Property(a => a.PageTitle)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired(true);


        builder.Property(a => a.Contnet)
                .HasColumnType("VARCHAR")
                .HasMaxLength(250)
                .IsRequired(true);

        builder.Property(a => a.Heading)
                .HasColumnType("VARCHAR")
                .HasMaxLength(250)
                .IsRequired(true);


        builder.Property(a => a.ShortDescription)
            .HasColumnType("VARCHAR")
            .HasMaxLength(70)
            .IsRequired(false);


        builder.Property(a => a.Author)
           .HasColumnType("VARCHAR")
            .IsRequired(true);

        builder.ToTable("Articles");


    }
}
