using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alb.Models
{
    public class Album
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int? CoverPhotoId { get; set; }
        public IEnumerable<int> Photos { get; set; }

        public Album() 
        {
            Photos = new List<int>();
        }
    }
}
