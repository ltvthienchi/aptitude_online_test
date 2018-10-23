using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AptitudeTestOnline.Models
{

    [Table("Exam")]
    public class ExamModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ExamID { get; set; }

        [Required]
        public string ExamName { get; set; }

        [Required]
        public string Description { get; set; }

    }
}