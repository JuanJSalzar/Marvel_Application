using System.Collections.Generic;
using System.Threading.Tasks;
using Marvel_Application.Models;

namespace Marvel_Application.Controllers
{
    public class CharactersController : ICharactersController
    {
        public async Task<List<Character>> GetSelectedCharactersAsync(int totalToFetch, int selectCount)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Character>> SearchCharactersAsync(string searchTerm, int selectCount)
        {
            throw new System.NotImplementedException();
        }
    }
}