using System.Threading.Tasks;
using Marvel_Application.Models;
using System.Collections.Generic;

namespace Marvel_Application.IServices
{
    public interface IMarvelService
    {
        Task<List<Character>> GetCharactersAsync(int limit, int offset);
        Task<List<Character>> SearchCharactersAsync(string searchTerm, int limit, int offset); 
    }
}