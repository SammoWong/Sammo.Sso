using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sammo.Sso.Domain.Entities;

namespace Sammo.Sso.Infrastructure.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            //设置表名
            builder.ToTable(nameof(Menu));

            //设置主键
            builder.HasKey(e => e.Id);

            //设置字段属性
            builder.Property(e => e.Id).HasColumnName(nameof(Menu.Id))
                .IsRequired()
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.ApplicationId).HasColumnName(nameof(Menu.ApplicationId))
                .IsRequired()
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.ParentId).HasColumnName(nameof(Menu.ParentId))
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.Name).HasColumnName(nameof(Menu.Name))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralStringLength})");

            builder.Property(e => e.Code).HasColumnName(nameof(Menu.Code))
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Category).HasColumnName(nameof(Menu.Category)).HasDefaultValue(MenuType.Catelog);

            builder.Property(e => e.Url).HasColumnName(nameof(Menu.Url))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralTextLength})");

            builder.Property(e => e.Icon).HasColumnName(nameof(Menu.Icon))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralStringLength})");

            builder.Property(e => e.Rank).HasColumnName(nameof(Menu.Rank)).HasDefaultValue(1);

            builder.Property(e => e.SortNumber).HasColumnName(nameof(Menu.SortNumber)).HasDefaultValue(1);

            builder.Property(e => e.IsExpanded).HasColumnName(nameof(Menu.IsExpanded)).HasDefaultValue(false);

            builder.Property(e => e.Enabled).HasColumnName(nameof(Menu.Enabled)).HasDefaultValue(true);

            builder.Property(e => e.Remark).HasColumnName(nameof(Menu.Remark))
                .HasColumnType($"VARCHAR({Domain.Constants.Validation.EntityValidator.GeneralTextLength})");

            builder.Property(e => e.CreatedBy).HasColumnName(nameof(Permission.CreatedBy))
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.CreatedTime).HasColumnName(nameof(Permission.CreatedTime))
                .HasColumnType("DATETIME");

            builder.Property(e => e.ModifiedBy).HasColumnName(nameof(Permission.ModifiedBy))
                .HasColumnType($"CHAR({Domain.Constants.Validation.EntityValidator.GuidStringLength})");

            builder.Property(e => e.ModifiedTime).HasColumnName(nameof(Permission.ModifiedTime))
                .HasColumnType("DATETIME");

            //设置表之间关系
            builder.HasMany(e => e.Buttons).WithOne(e => e.Menu).HasForeignKey(e => e.MenuId);
        }
    }
}
