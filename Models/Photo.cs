using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alb.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Data { get; set; }
        public IEnumerable<int> Albums { get; set; }

        public Photo()
        {
            Albums = new List<int>();
        }
    }
}
