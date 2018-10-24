using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptitudeTestOnline.Models
{
    public class DetailsRegistrations
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RegistrationID{ get; set; }
        public int AccountID { get; set; }
        public int ScheduleID{ get; set; }
        public int Mark { get; set; }

    }
}