using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sammo.Sso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sammo.Sso.Infrastructure.Data.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            //设置表名
            builder.ToTable(nameof(Application));

            //设置主键
            builder.HasKey(e => e.Id);

            //设置字段属性
            builder.Property(e => e.Id).HasColumnName(nameof(Application.Id))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.ClientId).HasColumnName(nameof(Application.ClientId))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.ClientName).HasColumnName(nameof(Application.ClientName))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.ClientSecrets).HasColumnName(nameof(Application.ClientSecrets))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.RedirectUris).HasColumnName(nameof(Application.RedirectUris))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralTextLength);

            builder.Property(e => e.PostLogoutRedirectUris).HasColumnName(nameof(Application.PostLogoutRedirectUris))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralTextLength);

            builder.Property(e => e.DisplayName).HasColumnName(nameof(Application.DisplayName))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Remark).HasColumnName(nameof(Application.Remark))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralTextLength);

            builder.Property(e => e.CreatedBy).HasColumnName(nameof(Application.CreatedBy))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.CreatedTime).HasColumnName(nameof(Application.CreatedTime))
                .HasColumnType("DATETIME");

            builder.Property(e => e.ModifiedBy).HasColumnName(nameof(Application.ModifiedBy))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.ModifiedTime).HasColumnName(nameof(Application.ModifiedTime))
                .HasColumnType("DATETIME");

            //设置表之间关系
            builder.HasMany(e => e.Menus).WithOne(e => e.Application).HasForeignKey(e => e.ApplicationId);
        }
    }
}
