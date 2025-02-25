using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);

            builder.Property(si => si.Id)
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd();

            builder.Property(si => si.SaleId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.Property(si => si.ProductId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(si => si.Quantity)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(si => si.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(si => si.Discount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0.0m);

            builder.Property(si => si.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("TotalAmount");

            // Relacionamento com Product
            builder.HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Sale
            builder.HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade); // Ao excluir uma venda, exclui os itens
        }
    }
}
