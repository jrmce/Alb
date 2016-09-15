using System.ComponentModel.DataAnnotations;

namespace Alb.Models
{
    public class Album
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
