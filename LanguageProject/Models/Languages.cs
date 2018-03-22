﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models
{
    public class Languages
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string FlagImgPath { get; set; }

    }
}