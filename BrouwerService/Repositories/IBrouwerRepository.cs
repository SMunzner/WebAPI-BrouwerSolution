using BrouwerService.Models;

namespace BrouwerService.Repositories
{
    public interface IBrouwerRepository
    {
        //IQueryable<Brouwer> FindAll();
        //Brouwer? FindById(int id);
        //IQueryable<Brouwer> FindByBeginNaam(string begin);
        //void Insert(Brouwer brouwer);
        //void Delete(Brouwer brouwer);
        //void Update(Brouwer brouwer);

        Task<List<Brouwer>> FindAllAsync();
        Task<Brouwer?> FindByIdAsync(int id);
        Task<List<Brouwer>> FindByBeginNaamAsync(string begin);
        Task InsertAsync(Brouwer brouwer);
        Task DeleteAsync(Brouwer brouwer);
        Task UpdateAsync(Brouwer brouwer);
    }
}
