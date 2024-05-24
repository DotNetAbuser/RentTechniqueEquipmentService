namespace Domain.Configurations;

public class OrderConfiguration
    : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId);
        builder
            .HasOne(x => x.Equipment)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.EquipmentId);
    }
}