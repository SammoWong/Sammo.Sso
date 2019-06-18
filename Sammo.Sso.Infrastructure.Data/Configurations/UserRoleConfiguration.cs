using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sammo.Sso.Domain.Entities;

namespace Sammo.Sso.Infrastructure.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //设置表名
            builder.ToTable(nameof(UserRole));

            //设置主键
            builder.HasKey(e => e.Id);

            //设置字段属性
            builder.Property(e => e.Id).HasColumnName(nameof(UserRole.Id))
                .IsRequired()
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.UserId).HasColumnName(nameof(UserRole.UserId))
                .IsRequired()
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.RoleId).HasColumnName(nameof(UserRole.RoleId))
                .IsRequired()
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            //设置表之间关系
            //User与UserRole一对多关系
            builder.HasOne(e => e.User).WithMany(e => e.UserRoles).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            //Role与UserRole一对多关系
            builder.HasOne(e => e.Role).WithMany(e => e.UserRoles).HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
