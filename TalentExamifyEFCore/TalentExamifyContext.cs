using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace TalentExamifyEFCore
{
    public class TalentExamifyContext :DbContext
    {
        public TalentExamifyContext(DbContextOptions<TalentExamifyContext> options) : base(options)
        {
        }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
