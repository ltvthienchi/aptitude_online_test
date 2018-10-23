using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AptitudeTestOnline.Models
{
    [Table("DetailsExam")]
    public class DetailsExamModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DetailsID { get; set; }

        [Required]
        public int ExamID { get; set; }

        [Required]
        public int QuestionsID { get; set; }
    }
}