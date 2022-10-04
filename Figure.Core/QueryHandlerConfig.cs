using Figure.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Figure.Core;
public static class QueryHandlerConfig {
    public static IServiceCollection AddQueryHandler<TQuery, TResult, TQueryHandler>(this IServiceCollection services)
        where TQueryHandler : class,IQueryHandler<TQuery,TResult>{
        services.AddTransient<IQueryHandler<TQuery, TResult>, TQueryHandler>();

        return services;
    }
}

