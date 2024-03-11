using BrouwerService.Models;

namespace BrouwerService.Repositories
{
    public interface IFiliaalRepository
    {
        Task<List<Filiaal>> FindAllAsync();
    }
}
