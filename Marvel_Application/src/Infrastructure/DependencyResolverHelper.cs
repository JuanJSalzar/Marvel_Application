using Unity;
using System;
using System.Web;

namespace Marvel_Application.Infrastructure
{
    public static class DependencyResolverHelper
    {
        public static T Resolve<T>()
        {
            var container = (IUnityContainer)HttpContext.Current.Application["UnityContainer"];
            return container == null ? throw new InvalidOperationException("Unity container not initialized.") : container.Resolve<T>();
        }
    }
}