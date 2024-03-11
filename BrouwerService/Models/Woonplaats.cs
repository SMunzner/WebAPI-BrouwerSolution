namespace BrouwerService.Models
{
    public class Woonplaats
    {
        public int Id { init; get; }
        public int Postcode { init; get; }
        public required string Naam { init; get; }

        //navigation property
        public required List<Filiaal> Filialen { init; get; }
    }
}
