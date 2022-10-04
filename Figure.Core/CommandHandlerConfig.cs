using Figure.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Figure.Core;

public static class CommandHandlerConfig {
    public static IServiceCollection AddCommandHandler<TCommand,TCommandHandler>(this IServiceCollection services)
        where TCommandHandler : class,ICommandHandler<TCommand>{
        services.AddTransient<ICommandHandler<TCommand>, TCommandHandler>();

        return services;
    }
}

