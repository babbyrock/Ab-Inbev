using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(r => r.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Image)
                .HasMaxLength(255);

            builder.HasOne(p => p.Rating)
            .WithOne()  // Não há propriedade de navegação em Rating de volta para Product
            .HasForeignKey<Rating>(r => r.ProductId)  // A chave estrangeira é ProductId
            .OnDelete(DeleteBehavior.Cascade);  // Quando um Product for deletado, seu Rating também será deletado
        }
    }
}
