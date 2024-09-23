using System.Configuration;
using Marvel_Application.IServices;

namespace Marvel_Application.Services
{
    public class MarvelConfig : IMarvelConfig
    {
        public string PublicKey => ConfigurationManager.AppSettings["MarvelPublicKey"];
        public string PrivateKey { get; } = ConfigurationManager.AppSettings["MarvelPrivateKey"];
    }
}