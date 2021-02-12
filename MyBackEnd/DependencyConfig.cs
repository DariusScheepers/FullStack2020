using Unity;
using MyBackEnd.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackEnd
{
    public static class DependencyConfig
    {
        public static IUnityContainer Container;

        public static void RegisterDomainDependencies(this IUnityContainer container)
        {
            container.RegisterType<IDatabase, Database>();
            Container = container;
        }
    }
}
