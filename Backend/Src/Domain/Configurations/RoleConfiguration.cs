namespace Domain.Configurations;

public class RoleConfiguration
    : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasIndex(x => x.Name)
            .IsUnique();

        builder
            .HasMany(x => x.Users)
            .WithOne(x => x.Role);

        builder.HasData(new List<RoleEntity> {
            new() 
            {
                Id = (int)RoleEnum.Guest,
                Name = RoleEnum.Guest.ToString()
            },
            new()
            {
                Id = (int)RoleEnum.Operator,
                Name = RoleEnum.Operator.ToString()
            },
            new()
            {
                Id = (int)RoleEnum.Admin,
                Name = RoleEnum.Admin.ToString()
            }
        });
    }
}