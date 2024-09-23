using System.Collections.Generic;
using System.Threading.Tasks;
using Marvel_Application.IServices;
using Marvel_Application.Models;

namespace Marvel_Application.Services
{
    public class MarvelService : IMarvelService
    {
        public async Task<List<Character>> GetCharactersAsync(int limit, int offset)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Character>> SearchCharactersAsync(string searchTerm, int limit, int offset)
        {
            throw new System.NotImplementedException();
        }
    }
}