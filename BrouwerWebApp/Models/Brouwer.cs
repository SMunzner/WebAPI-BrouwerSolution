namespace BrouwerWebApp.Models
{
    public class Brouwer
    {
        public int Id { init; get; }
        public required string Naam { init; get; }
        public int Postcode { init; get; }
        public required string Gemeente { init; get; }

    }
}
