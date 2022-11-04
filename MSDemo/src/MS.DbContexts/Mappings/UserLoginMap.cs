using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;

namespace MS.DbContexts.Mappings
{
    public class UserLoginMap : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("TblUserLogins");
            builder.HasKey(u => u.Account);
            builder.Property(u => u.Account).IsRequired().HasMaxLength(20);
            builder.Property(u => u.HashedPassword).IsRequired().HasMaxLength(256);
            builder.Property(u => u.LastLoginTime);
            builder.Property(u => u.AccessFailedCount).IsRequired().HasMaxLength(0);
            builder.Property(u => u.IsLocked).IsRequired();
            builder.Property(u => u.LockedTime);

            builder.HasOne(u => u.User);
        }
    }
}
