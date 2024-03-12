using BrouwerWebApp.Models;
using BrouwerWebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BrouwerWebApp.Repositories
{
    public class BrouwerRepository(BierlandContext context) : IBrouwerRepository
    {
        public async Task<List<Brouwer>> FindAllAsync() =>
            await context.Brouwers.AsNoTracking().ToListAsync();
        
        public async Task<Brouwer?> FindByIdAsync(int id) =>
            await context.Brouwers.FindAsync(id);
    }
}
