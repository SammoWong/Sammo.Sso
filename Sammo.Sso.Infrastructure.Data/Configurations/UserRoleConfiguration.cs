using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sammo.Sso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.Id).HasColumnName(nameof(UserRole.UserId))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.Id).HasColumnName(nameof(UserRole.RoleId))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            //设置表之间关系
            //builder.HasOne(e=>e.User).WithMany(e=>e)
        }
    }
}
