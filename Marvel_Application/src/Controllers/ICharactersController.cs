using System.Threading.Tasks;
using Marvel_Application.Models;
using System.Collections.Generic;

namespace Marvel_Application.Controllers
{
    public interface ICharactersController
    {
        Task<List<Character>> GetSelectedCharactersAsync(int totalToFetch, int selectCount);
        Task<List<Character>> SearchCharactersAsync(string searchTerm, int selectCount);
    }
}