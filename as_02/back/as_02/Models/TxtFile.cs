using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace as_02.Models
{
    public class TxtFile
    {
        [Required]
        public string Name { get; set; }
        public string Text { get; set; }
        [Required]
        public string Type { get; set; }
        
        public bool IsValid()
        {
            string pattern = "";
            switch (Type)
            {
                case "ruFiles":
                    pattern = "[^а-яА-Я]";
                    break;
                case "engFiles":
                    pattern = "[^a-zA-Z]";
                    break;
                case "digitFiles":
                    pattern = "[^0-9]";
                    break;
            }
            return !Regex.IsMatch(Text, pattern, RegexOptions.IgnoreCase);
        }

    }
}