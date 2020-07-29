using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserServiceAccount.Domain.Entities;

namespace UserServiceAccount.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasColumnName("UserName");

            builder.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("Password");
        }
    }
}
