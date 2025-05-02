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
    public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
    {
        public void Configure(EntityTypeBuilder<ExamResult> builder)
        {
            builder.Property(er => er.Score).IsRequired();
            builder.Property(er => er.SubmittedAt).IsRequired();

            builder.HasOne(er => er.User)
                   .WithMany(u => u.ExamResults)
                   .HasForeignKey(er => er.UserId);

            builder.HasOne(er => er.Exam)
                   .WithMany(e => e.ExamResults)
                   .HasForeignKey(er => er.ExamId);
        }
    }
}
