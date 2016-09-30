using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alb.Models
{
    public class CreatePhoto
    {
        [Required]
        public int Size { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Data { get; set; }
        public IEnumerable<int> Albums { get; set; }

        public CreatePhoto()
        {
            Albums = new List<int>();
        }
    }
}