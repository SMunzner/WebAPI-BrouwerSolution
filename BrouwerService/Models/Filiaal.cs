namespace BrouwerService.Models
{
    public class Filiaal
    {
        public int Id { get; set; }
        public required string Naam { init; get; }
        public int HuurPrijs { init; get; }
        public int WoonPlaatsId { init; get; }
        public required Woonplaats Woonplaats { init; get; }
    }
}
