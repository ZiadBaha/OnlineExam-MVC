using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.Title).IsRequired().HasMaxLength(500);

            builder.HasMany(q => q.Choices)
                   .WithOne(c => c.Question)
                   .HasForeignKey(c => c.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
