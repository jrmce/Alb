namespace Alb.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url 
        {
            get
            {
                return $"/images/{Filename}";
            }
        }
        public string Filename { get; set; }
    }
}
