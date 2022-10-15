using Figure.Core;
using Figure.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Figure.Application;
public static class HandlerConfig {

    private static IEnumerable<Type> GetAllTypes(Type genericType) {
        if (!genericType.IsGenericTypeDefinition)
            throw new ArgumentException("Type must be generic", nameof(genericType));

        return Assembly.GetExecutingAssembly()
                       .GetTypes()
                       .Where(t => t.GetInterfaces()
                       .Any(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition().Equals(genericType)));
    }

    public static void RegisterAllHandlers(this IServiceCollection services) {
        var commandHandlers = GetAllTypes(typeof(ICommandHandler<>));
        var queryHandlers = GetAllTypes(typeof(IQueryHandler<,>));

        foreach(var handler in commandHandlers) {
            services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)),handler);
        }

        foreach (var handler in queryHandlers) {
            services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)), handler);
        }
    }
}

