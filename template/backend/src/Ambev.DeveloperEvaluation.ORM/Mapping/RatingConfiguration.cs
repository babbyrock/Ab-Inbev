using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings", t => t.HasCheckConstraint("CK_Rating_Rate", "Rate >= 1 AND Rate <= 5"));

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.ProductId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(r => r.Rate)
                .IsRequired()
                .HasColumnType("decimal(3,2)")
                .HasDefaultValue(0);

            builder.Property(r => r.Count)
                .HasColumnType("int");

            builder.Property(r => r.Date)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("NOW()");


            builder.HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
