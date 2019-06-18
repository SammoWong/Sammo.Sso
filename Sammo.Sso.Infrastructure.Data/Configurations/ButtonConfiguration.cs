using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sammo.Sso.Domain.Entities;

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
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.MenuId).HasColumnName(nameof(Button.MenuId))
                .IsRequired()
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.Name).HasColumnName(nameof(Button.Name))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralStringLength})");

            builder.Property(e => e.Code).HasColumnName(nameof(Button.Code))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralStringLength})");

            builder.Property(e => e.JsEvent).HasColumnName(nameof(Button.JsEvent))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralStringLength})");

            builder.Property(e => e.Url).HasColumnName(nameof(Button.Url))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralTextLength})");

            builder.Property(e => e.Icon).HasColumnName(nameof(Button.Icon))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralStringLength})");

            builder.Property(e => e.SortNumber).HasColumnName(nameof(Button.SortNumber)).HasDefaultValue(1);

            builder.Property(e => e.Enabled).HasColumnName(nameof(Button.Enabled)).HasDefaultValue(true);

            builder.Property(e => e.Remark).HasColumnName(nameof(Button.Remark))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralTextLength})");

            builder.Property(e => e.CreatedBy).HasColumnName(nameof(Button.CreatedBy))
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.CreatedTime).HasColumnName(nameof(Button.CreatedTime))
                .HasColumnType("DATETIME");

            builder.Property(e => e.ModifiedBy).HasColumnName(nameof(Button.ModifiedBy))
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.ModifiedTime).HasColumnName(nameof(Button.ModifiedTime))
                .HasColumnType("DATETIME");
        }
    }
}
