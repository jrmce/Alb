using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alb.Models
{
    public class CreateShare
    {
        public string Name { get; set; }
        [Required]
        public DateTime ExpiredAt { get; set; }
        public string Password { get; set; }
        public IEnumerable<int> Photos { get; set; }
        public IEnumerable<int> Albums { get; set; }

        public CreateShare()
        {
            Photos = new List<int>();
            Albums = new List<int>();
        }
    }

    
}