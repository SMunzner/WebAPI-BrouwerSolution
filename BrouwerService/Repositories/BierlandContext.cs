using Microsoft.EntityFrameworkCore;
using BrouwerService.Models;

namespace BrouwerService.Repositories;
public class BierlandContext(DbContextOptions<BierlandContext> options) : 
    DbContext(options)
{
    public required DbSet<Brouwer> Brouwers { init; get; }
}

