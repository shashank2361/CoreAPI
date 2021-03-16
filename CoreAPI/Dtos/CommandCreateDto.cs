﻿using CoreAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Dtos
{
    public class CommandCreateDto 
        //: IEntity    --- This is to trigger the NOTFOUNDAction filter
    {
        //[Required]
        //public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        public string Platform { get; set; }

        [Required]
        public string CommandLine { get; set; }
     }
}