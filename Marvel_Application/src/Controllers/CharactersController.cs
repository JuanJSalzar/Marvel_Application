using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Marvel_Application.IServices;
using Marvel_Application.Models;

namespace Marvel_Application.Controllers
{
    public class CharactersController : ICharactersController
    {
        private readonly IMarvelService _marvelService;

        public CharactersController(IMarvelService marvelService)
        {
            _marvelService = marvelService;
        }
        
        public async Task<List<Character>> GetSelectedCharactersAsync(int totalToFetch, int selectCount)
        {
            int batchSize = 100;
            int numberOfBatches = (int)Math.Ceiling((double)totalToFetch / batchSize); 
            var tasks = new List<Task<List<Character>>>();

            for (int i = 0; i < numberOfBatches; i++)
            {
                int offset = i * batchSize;
                Trace.WriteLine($"[CharacterController] Fetching characters: limit={batchSize}, offset={offset}");
                tasks.Add(_marvelService.GetCharactersAsync(batchSize, offset));
            }

            var results = await Task.WhenAll(tasks);
            var allCharacters = results.SelectMany(x => x).ToList();

            var validCharacters = allCharacters
                .Where(c => c.Thumbnail != null
                            && !string.IsNullOrEmpty(c.Thumbnail.Path)
                            && !c.Thumbnail.Path.Contains("image_not_available")
                            && !c.Thumbnail.Extension.Equals("gif", StringComparison.OrdinalIgnoreCase))
                .ToList();
            Trace.WriteLine($"[CharacterController] Total valid characters fetched: {validCharacters.Count}");

            var selectedCharacters = GetRandomCharacters(validCharacters, selectCount);
            return selectedCharacters;
        }

        public async Task<List<Character>> SearchCharactersAsync(string searchTerm, int selectCount)
        {
            int totalToFetch = 200; 
            int batchSize = 100; 
            int numberOfBatches = (int)Math.Ceiling((double)totalToFetch / batchSize);
            var tasks = new List<Task<List<Character>>>();

            for (int i = 0; i < numberOfBatches; i++)
            {
                int offset = i * batchSize;
                tasks.Add(_marvelService.SearchCharactersAsync(searchTerm, batchSize, offset));
            }

            var results = await Task.WhenAll(tasks);
            var allCharacters = results.SelectMany(x => x).ToList();

            var validCharacters = allCharacters
                .Where(c => c.Thumbnail != null
                            && !string.IsNullOrEmpty(c.Thumbnail.Path)
                            && !c.Thumbnail.Path.Contains("image_not_available")
                            && !c.Thumbnail.Extension.Equals("gif", StringComparison.OrdinalIgnoreCase))
                .ToList();

            Trace.WriteLine($"[CharacterController] Total valid characters found in search: {validCharacters.Count}");

            var selectedCharacters = GetRandomCharacters(validCharacters, selectCount);
            return selectedCharacters;
        }

        private List<Character> GetRandomCharacters(List<Character> characters, int count)
        {
            var random = new Random();
            return characters.OrderBy(x => random.Next()).Take(count).ToList();
        }
    }
}