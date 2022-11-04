using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;

namespace MS.DbContexts.Mappings
{
    public class LogrecordMap : IEntityTypeConfiguration<Logrecord>
    {
        public void Configure(EntityTypeBuilder<Logrecord> builder)
        {
            builder.ToTable("TblLogrecords");
            builder.HasKey(l => l.Id);//自增主键
            builder.Property(l => l.LogDate).IsRequired();
            builder.Property(l => l.LogLevel).IsRequired().HasMaxLength(50);
            builder.Property(l => l.Logger).IsRequired().HasMaxLength(256);
            builder.Property(l => l.Message);
            builder.Property(l => l.Exception);
            builder.Property(l => l.MachineName).HasMaxLength(50);
            builder.Property(l => l.MachineIp).HasMaxLength(50);
            builder.Property(l => l.NetRequestMethod).HasMaxLength(10);
            builder.Property(l => l.NetRequestUrl).HasMaxLength(500);
            builder.Property(l => l.NetUserIsauthenticated).HasMaxLength(10);
            builder.Property(l => l.NetUserAuthtype).HasMaxLength(50);
            builder.Property(l => l.NetUserIdentity).HasMaxLength(50);
        }
    }
}
