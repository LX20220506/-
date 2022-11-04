using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;

namespace MS.DbContexts.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("TblRoles");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();// 取消自动生成
            builder.Property(r => r.Name).IsUnicode();//指定索引，不能重复;唯一约束
            builder.Property(r => r.Name).IsRequired().HasMaxLength(16);
            builder.Property(r => r.DisplayName).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Remark).HasMaxLength(4000);
            builder.Property(r => r.Creator).IsRequired();
            builder.Property(r => r.CreateTime).IsRequired();
            builder.Property(r => r.Modifier);
            builder.Property(r => r.ModifyTime);

            //builder.HasQueryFilter(r => r.StatusCode != Entities.Core.StatusCode.Delete);//默认不查询软删除数据

        }
    }
}
