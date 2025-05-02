using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(u => u.PhoneNumber)
                   .IsUnique(true);

            builder.HasIndex(u => u.Email)
                   .IsUnique(true); 

            builder.Property(u => u.Role)
                   .HasConversion<string>();

            builder.HasMany(u => u.ExamResults)
                   .WithOne(er => er.User)
                   .HasForeignKey(er => er.UserId);
        }
    }
}
