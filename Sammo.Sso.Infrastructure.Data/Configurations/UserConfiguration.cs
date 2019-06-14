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
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.NickName).HasColumnName(nameof(User.NickName))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);

            builder.Property(e => e.RealName).HasColumnName(nameof(User.RealName))
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(Domain.Constants.Validation.EntityValidator.GeneralStringLength);
        }
    }
}
