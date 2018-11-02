using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AptitudeTestOnline.Models
{
    public class CandidateAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CandidateAnswerID { get; set; }

        [Required]
        public int AccountID { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Required]
        public int Answer { get; set; }
    }
}