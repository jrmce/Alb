using System;

namespace Alb.Models
{
    public class Share
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Token { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
