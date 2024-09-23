using System.Web;
using Marvel_Application.Controllers;
using Marvel_Application.IServices;
using Marvel_Application.Services;
using Unity;
using Unity.Lifetime;

namespace Marvel_Application.Infrastructure
{
    
    public class UnityHttpModule : IHttpModule
    {
        private static IUnityContainer container;

        public void Init(HttpApplication context)
        {
            container = new UnityContainer();
            container.RegisterType<IMarvelConfig, MarvelConfig>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMarvelService, MarvelService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICharactersController, CharactersController>(new HierarchicalLifetimeManager());

            context.Application["UnityContainer"] = container;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public static IUnityContainer GetContainer()
        {
            return container;
        }
    }
}