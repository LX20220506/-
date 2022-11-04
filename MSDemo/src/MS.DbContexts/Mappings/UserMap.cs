using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;
using MS.Entities.Core;

namespace MS.DbContexts.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TblUsers");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedNever();// 取消自动生成id
            builder.Property(u => u.Account).IsUnicode();// 唯一约束
            builder.Property(u => u.Account).IsRequired().HasMaxLength(16);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.Phone).HasMaxLength(25);
            builder.Property(u => u.RoleId).IsRequired();
            builder.Property(u => u.StatusCode).IsRequired().HasDefaultValue(StatusCode.Enable);
            builder.Property(u => u.Creator).IsRequired();
            builder.Property(u => u.CreateTime).IsRequired();
            builder.Property(u => u.Modifier);
            builder.Property(u => u.ModifyTime);

            builder.HasOne(u => u.Role);

            //builder.HasQueryFilter(u => u.StatusCode != StatusCode.Delete);//默认不查询软删除数据
        }
    }
}
