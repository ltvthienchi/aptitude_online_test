using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AptitudeTestOnline.Models
{
    public class ATODatabaseContext : DbContext
    {
        public ATODatabaseContext() : base("name=DefaultConnection") { }
        public DbSet<TypeOfQuestionModel> TypeOfQuestionModel { get; set; }
        public DbSet<QuestionsModels> QuestionsModels { get; set; }
        public DbSet<AccountModel> AccountModels { get; set; }
        public DbSet<ExamModels> ExamModels { get; set; }
        public DbSet<DetailsExamModels> DetailsExamModels { get; set; }
    }
}