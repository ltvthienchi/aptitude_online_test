﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AptitudeTestOnline.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }

        [Required]

        public string Education { get; set; }

        [Required]

        public string Experience { get; set; }

        [Required]

        public string Interest { get; set; }

        [Required]

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string UserID { get; set; }
    }
}