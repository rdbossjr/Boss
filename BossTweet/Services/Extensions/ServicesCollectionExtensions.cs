using System.Reflection;

namespace BossTweet.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterAsTransientByConvention(this IServiceCollection services, string assemblyName, Func<Type, bool> typeFilter)
    {
        var interfaces = new List<Type>();
        var implementations = new List<Type>();

        var assembly = AppDomain.CurrentDomain.GetAssemblies()
            .SingleOrDefault(a => a.GetName().Name.Equals(assemblyName, StringComparison.OrdinalIgnoreCase));

        if (assembly == null)
        {
            assembly = Assembly.Load(assemblyName);
        }

        //interfaces.AddRange(assembly.ExportedTypes.Where(x => x.IsInterface && typeFilter(x)));
        interfaces.AddRange(assembly.ExportedTypes.Where(x => x.IsInterface && typeFilter(x)));

        implementations.AddRange(assembly.ExportedTypes.Where(x => !x.IsInterface && !x.IsAbstract
            && typeFilter(x)));

        foreach (var currentInterface in interfaces)
        {
            var implementation = implementations
                .FirstOrDefault(x => currentInterface.IsAssignableFrom(x) &&
                                     $"I{x.Name}" == currentInterface.Name);

            if (implementation == null)
            {
                continue;
            }

            services.AddTransient(currentInterface, implementation);
        }
    }

}