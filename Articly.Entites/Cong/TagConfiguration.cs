using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articly.Entites.Cong
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.TagId);




            //Columns
            builder.Property(a => a.Name)

                      .HasColumnType("VARCHAR")
                      .HasMaxLength(250)

                      .IsRequired(true);



            builder.Property(a => a.DisplayName)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(50)
                    .IsRequired(true);


            builder.HasIndex(t => t.DisplayName).IsUnique();

            builder.ToTable("Tags");


        }
    }
}
