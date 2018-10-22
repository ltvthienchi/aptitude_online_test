using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AptitudeTestOnline.Models
{
    public class ATODatabaseContext: DbContext
    {
        public ATODatabaseContext() : base("name=DefaultConnection") { }
        public DbSet<TypeOfQuestionModel> TypeOfQuestionModel { get; set; }
    }
}