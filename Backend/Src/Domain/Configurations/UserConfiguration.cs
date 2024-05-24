namespace Domain.Configurations;

public class UserConfiguration
    : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasIndex(x => x.Email)
            .IsUnique();
        builder
            .HasIndex(x => x.PhoneNumber)
            .IsUnique();

        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId);
        builder
            .HasMany(x => x.Sessions)
            .WithOne(x => x.User);

        builder.HasData(new List<UserEntity> {
            new()
            {
                Id = Guid.NewGuid(),
                RoleId = (int)RoleEnum.Guest,
                LastName = "Салахиев",
                FirstName = "Булат",
                MiddleName = "Гость",
                Email = "bulat1@example.com",
                PhoneNumber = "+79177793601",
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("12032003")
            },
            new()
            {
                Id = Guid.NewGuid(),
                RoleId = (int)RoleEnum.Operator,
                LastName = "Салахиев",
                FirstName = "Булат",
                MiddleName = "Оператор",
                Email = "bulat2@example.com",
                PhoneNumber = "+79177793602",
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("12032003")
            },
            new()
            {
                Id = Guid.NewGuid(),
                RoleId = (int)RoleEnum.Admin,
                LastName = "Салахиев",
                FirstName = "Булат",
                MiddleName = "Админ",
                Email = "bulat3@example.com",
                PhoneNumber = "+79177793603",
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("12032003")
            }
        });
    }
}