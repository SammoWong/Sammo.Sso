using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sammo.Sso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sammo.Sso.Infrastructure.Data.Configurations
{
    public class ButtonConfiguration : IEntityTypeConfiguration<Button>
    {
        public void Configure(EntityTypeBuilder<Button> builder)
        {
            //设置表名
            builder.ToTable(nameof(Button));

            //设置主键
            builder.HasKey(e => e.Id);

            //设置字段属性
            builder.Property(e => e.Id).HasColumnName(nameof(Button.Id))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.MenuId).HasColumnName(nameof(Button.MenuId))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.Name).HasColumnName(nameof(Button.Name))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Code).HasColumnName(nameof(Button.Code))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.JsEvent).HasColumnName(nameof(Button.JsEvent))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Url).HasColumnName(nameof(Button.Url))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralTextLength);

            builder.Property(e => e.Icon).HasColumnName(nameof(Button.Icon))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.SortNumber).HasColumnName(nameof(Button.SortNumber));

            builder.Property(e => e.Enabled).HasColumnName(nameof(Button.Enabled));

            builder.Property(e => e.Remark).HasColumnName(nameof(Button.Remark))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralTextLength);

            builder.Property(e => e.CreatedBy).HasColumnName(nameof(Button.CreatedBy))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.CreatedTime).HasColumnName(nameof(Button.CreatedTime))
                .HasColumnType("DATETIME");

            builder.Property(e => e.ModifiedBy).HasColumnName(nameof(Button.ModifiedBy))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.ModifiedTime).HasColumnName(nameof(Button.ModifiedTime))
                .HasColumnType("DATETIME");
        }
    }
}
