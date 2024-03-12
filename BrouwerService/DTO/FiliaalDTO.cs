namespace BrouwerService.DTO
{
    //ipv direct de entities aan te spreken geef je hiermee zelf aan welke info
    //mag gezien worden in SwashBuckle (bijvoorbeeld)


    public record FiliaalDTO(int Id, string Naam, int Postcode, string Woonplaats)
    {
    }
}
