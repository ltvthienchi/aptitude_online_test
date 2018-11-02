using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AptitudeTestOnline.Models
{
    [Table("PerformTest")]
    public class PerformTest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PerformTestID { get; set; }

        [Required]
        public int AccountID { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true), Required]
        public DateTime AddedDate { get; set; }

        [Required]
        public int ExamID { get; set; }

        [Required]
        public string TimePartOne { get; set; }

        [Required]
        public string TimePartTwo { get; set; }

        [Required]
        public string TimePartThree { get; set; }
        
    }
}