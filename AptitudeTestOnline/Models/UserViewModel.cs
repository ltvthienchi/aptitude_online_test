using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptitudeTestOnline.Models
{
    public class UserViewModel
    {
        [Key]
        public int AccountID { get; set; }
        public string Name { get; set; }

        public string Education { get; set; }
        public string Experience { get; set; }
        public int Mark { get; set; }
    }
}