using System.Web;
using Unity;

namespace Marvel_Application.Infrastructure
{
    public static class DependencyResolver
    {
        public static T Resolve<T>()
        {
            var container = (IUnityContainer)HttpContext.Current.Application["UnityContainer"];
            return container.Resolve<T>();
        }
    }
}