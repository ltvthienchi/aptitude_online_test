using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptitudeTestOnline.Models
{
    public class QuestionsModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int QuestionsID { get; set; }
        public string QuestionsName { get; set; }
        public int TypeOfQuestion { get; set; }
        public string AnswerOne { get; set; }
        public string AnswerTwo { get; set; }
        public string AnswerThree { get; set; }
        public string AnswerFour { get; set; }
        public int CorrectAnswer { get; set; }
        public int Mark { get; set; }
    }
}