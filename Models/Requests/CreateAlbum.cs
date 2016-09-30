using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alb.Models
{
    public class CreateAlbum
    {
        [Required]
        public string Title { get; set; }
        public int? CoverPhotoId { get; set; }
        public IEnumerable<int> Photos { get; set; }

        public CreateAlbum()
        {
            Photos = new List<int>();
        }
    }
}