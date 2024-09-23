using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Marvel_Application.IServices;
using Marvel_Application.Models;
using Newtonsoft.Json;

namespace Marvel_Application.Services
{
    public class MarvelService : IMarvelService
    {
        private readonly IMarvelConfig _config;
        private readonly HttpClient _httpClient;

        public MarvelService(IMarvelConfig config, HttpClient httpClient = null)
        {
            _config = config;
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task<List<Character>> GetCharactersAsync(int limit, int offset)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var hash = GenerateHash(ts, _config.PrivateKey, _config.PublicKey);

            var requestUrl = $"https://gateway.marvel.com:443/v1/public/characters?limit={limit}&offset={offset}&ts={ts}&apikey={_config.PublicKey}&hash={hash}";
            try
            {
                var response = await _httpClient.GetStringAsync(requestUrl);

                var result = JsonConvert.DeserializeObject<MarvelApiResponse>(response);
                var characters = result?.Data?.Results
                   .Where(c => c.Thumbnail != null
                               && !string.IsNullOrEmpty(c.Thumbnail.Path)
                               && !c.Thumbnail.Path.Contains("image_not_available")
                               && !c.Thumbnail.Extension.Equals("gif", StringComparison.OrdinalIgnoreCase))
                   .ToList() ?? new List<Character>();

                Trace.WriteLine($"[MarvelService] Fetched {characters.Count} valid characters from Marvel API.");
                return characters;

            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Request error: {httpEx.Message}");               
                return new List<Character>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Character>();
            }
        }

        public async Task<List<Character>> SearchCharactersAsync(string searchTerm, int limit, int offset)
        {
            var ts = DateTime.Now.Ticks.ToString();
            var hash = GenerateHash(ts, _config.PrivateKey, _config.PublicKey);

            var requestUrl = $"https://gateway.marvel.com:443/v1/public/characters?name={Uri.EscapeDataString(searchTerm)}&limit={limit}&offset={offset}&ts={ts}&apikey={_config.PublicKey}&hash={hash}";

            try
            {
                var response = await _httpClient.GetStringAsync(requestUrl);

                var result = JsonConvert.DeserializeObject<MarvelApiResponse>(response);
                var characters = result?.Data?.Results
                   .Where(c => c.Thumbnail != null
                               && !string.IsNullOrEmpty(c.Thumbnail.Path)
                               && !c.Thumbnail.Path.Contains("image_not_available")
                               && !c.Thumbnail.Extension.Equals("gif", StringComparison.OrdinalIgnoreCase))
                   .ToList() ?? new List<Character>();

                Trace.WriteLine($"[MarvelService] Fetched {characters.Count} valid characters from Marvel API search.");
                return characters;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Request error: {httpEx.Message}");
                return new List<Character>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Character>();
            }
        }

        private static string GenerateHash(string ts, string privateKey, string publicKey)
        {
            var toBeHashed = ts + privateKey + publicKey;
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(toBeHashed); 
                var hashBytes = md5.ComputeHash(bytes);
                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}