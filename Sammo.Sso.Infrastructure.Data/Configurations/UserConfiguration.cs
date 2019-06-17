using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sammo.Sso.Domain.Entities;

namespace Sammo.Sso.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //设置表名
            builder.ToTable(nameof(User));

            //设置主键
            builder.HasKey(e => e.Id);

            //设置字段属性
            builder.Property(e => e.Id).HasColumnName(nameof(User.Id))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.UserName).HasColumnName(nameof(User.UserName))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.NickName).HasColumnName(nameof(User.NickName))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.RealName).HasColumnName(nameof(User.RealName))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.IdCard).HasColumnName(nameof(User.IdCard))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Mobile).HasColumnName(nameof(User.Mobile))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Email).HasColumnName(nameof(User.Email))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Password).HasColumnName(nameof(User.Password))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.Salt).HasColumnName(nameof(User.Salt))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralLongerStringLength);

            builder.Property(e => e.Gender).HasColumnName(nameof(User.Gender));

            builder.Property(e => e.Birthday).HasColumnName(nameof(User.Birthday))
                .HasColumnType("DATETIME");

            builder.Property(e => e.Avatar).HasColumnName(nameof(User.Avatar))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralTextLength);

            builder.Property(e => e.Enabled).HasColumnName(nameof(User.Enabled));

            builder.Property(e => e.CreatedBy).HasColumnName(nameof(User.CreatedBy))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.CreatedTime).HasColumnName(nameof(User.CreatedTime))
                .HasColumnType("DATETIME");

            builder.Property(e => e.ModifiedBy).HasColumnName(nameof(User.ModifiedBy))
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GuidStringLength);

            builder.Property(e => e.ModifiedTime).HasColumnName(nameof(User.ModifiedTime))
                .HasColumnType("DATETIME");
        }
    }
}
