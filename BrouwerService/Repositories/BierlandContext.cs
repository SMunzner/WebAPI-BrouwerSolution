using Microsoft.EntityFrameworkCore;
using BrouwerService.Models;

namespace BrouwerService.Repositories;
public class BierlandContext(DbContextOptions<BierlandContext> options) : 
    DbContext(options)
{
    public required DbSet<Brouwer> Brouwers { init; get; }
    public required DbSet<Woonplaats> Woonplaatsen { init; get;}
    public required DbSet<Filiaal> Filialen { init; get; }
}

