﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sammo.Sso.Domain.Entities;

namespace Sammo.Sso.Infrastructure.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            //设置表名
            builder.ToTable(nameof(Permission));

            //设置主键
            builder.HasKey(e => e.Id);

            //设置字段属性
            builder.Property(e => e.Id).HasColumnName(nameof(Permission.Id))
                .IsRequired()
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.MasterType).HasColumnName(nameof(Permission.MasterType)).HasDefaultValue(MasterType.Role);

            builder.Property(e => e.MasterId).HasColumnName(nameof(Permission.MasterId))
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.AccessType).HasColumnName(nameof(Permission.AccessType)).HasDefaultValue(AccessType.Menu);

            builder.Property(e => e.AccessId).HasColumnName(nameof(Permission.AccessId))
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.SortNumber).HasColumnName(nameof(Permission.SortNumber)).HasDefaultValue(1);

            builder.Property(e => e.Enabled).HasColumnName(nameof(Permission.Enabled)).HasDefaultValue(true);

            builder.Property(e => e.CreatedBy).HasColumnName(nameof(Permission.CreatedBy))
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.CreatedTime).HasColumnName(nameof(Permission.CreatedTime))
                .HasColumnType("DATETIME");

            builder.Property(e => e.ModifiedBy).HasColumnName(nameof(Permission.ModifiedBy))
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.ModifiedTime).HasColumnName(nameof(Permission.ModifiedTime))
                .HasColumnType("DATETIME");
        }
    }
}
