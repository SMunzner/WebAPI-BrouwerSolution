using BrouwerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BrouwerWebApp.Repositories
{
    public class BierlandContext(DbContextOptions<BierlandContext> options) : 
        DbContext(options)
    {
        public required DbSet<Brouwer> Brouwers { init; get; }
    }
}
