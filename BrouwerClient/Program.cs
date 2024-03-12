
//Dit project stelt een andere client voor


using System.Net;
Console.Write("Druk enter om de namen te zien");
Console.ReadLine();
using var client = new HttpClient();
var response = await client.GetAsync($"http://localhost:5000/brouwers");
switch (response.StatusCode)
{
    case HttpStatusCode.OK:
        var brouwers = await response.Content.ReadAsAsync<List<Brouwer>>();
        brouwers.ForEach(brouwer => Console.WriteLine(brouwer.Naam));
        break;
    case HttpStatusCode.NotFound:
        Console.WriteLine("Brouwer niet gevonden");
        break;
    default:
        Console.WriteLine("Technisch probleem, contacteer de helpdesk.");
        break;
}


class Brouwer
{
    public int ID { get; set; }
    public required string Naam { get; set; }
    public int Postcode { get; set; }
    public required string Gemeente { get; set; }
}