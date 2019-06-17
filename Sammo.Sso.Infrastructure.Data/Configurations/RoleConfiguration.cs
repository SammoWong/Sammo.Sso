using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sammo.Sso.Domain.Entities;

namespace Sammo.Sso.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //设置表名
            builder.ToTable(nameof(Role));

            //设置主键
            builder.HasKey(e => e.Id);

            //设置字段属性
            builder.Property(e => e.Id).HasColumnName(nameof(Role.Id))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.Name).HasColumnName(nameof(Role.Name))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Code).HasColumnName(nameof(Role.Code))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Description).HasColumnName(nameof(Role.Description))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralLongestStringLength);

            builder.Property(e => e.Enabled).HasColumnName(nameof(Role.Enabled));

            builder.Property(e => e.CreatedBy).HasColumnName(nameof(Role.CreatedBy))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.CreatedTime).HasColumnName(nameof(Role.CreatedTime))
                .HasColumnType("DATETIME");

            builder.Property(e => e.ModifiedBy).HasColumnName(nameof(Role.ModifiedBy))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.ModifiedTime).HasColumnName(nameof(Role.ModifiedTime))
                .HasColumnType("DATETIME");
        }
    }
}
