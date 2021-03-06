using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace IoC
{
    public static class IoCConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            Configure(services, DAL.IoC.Module.GetTypes());
            Configure(services, DomainServices.IoC.Module.GetTypes());
        }

        private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var type in types)
            {
                services.AddScoped(type.Key, type.Value);
            }
        }
    }
}
