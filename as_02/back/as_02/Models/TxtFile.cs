using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace as_02.Models
{
    public class TxtFile
    {
        [Required]
        public string Name { get; set; }
        public string Text { get; set; }
        [Required]
        public string Type { get; set; }

    }
}