namespace BrouwerService.Models
{
    public class Brouwer
    {
        public int Id { get; set; }
        public required string Naam { get; set; }
        public int Postcode { get; set; }
        public required string Gemeente { get; set; };

    }
}
