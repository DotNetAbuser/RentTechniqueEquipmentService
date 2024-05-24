namespace Domain.Configurations;

public class OrderStatusConfiguration
    : IEntityTypeConfiguration<OrderStatusEntity>
{
    public void Configure(EntityTypeBuilder<OrderStatusEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasIndex(x => x.Name)
            .IsUnique();

        builder
            .HasMany(x => x.Orders)
            .WithOne(x => x.Status)
            .HasForeignKey(x => x.StatusId);

        builder.HasData(new List<OrderStatusEntity> {
            new()
            {
                Id = 1,
                Name = "В ожидание"
            },
            new()
            {
                Id = 2,
                Name = "В работе"
            },
            new()
            {
                Id = 3,
                Name = "Закончена"
            }
        });
    }
}