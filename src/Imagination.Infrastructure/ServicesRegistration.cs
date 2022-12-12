using Imagination.Domain.Image;
using Imagination.Infrastructure.Image;
using Microsoft.Extensions.DependencyInjection;

namespace Imagination.Infrastructure;

public static class ServicesRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IImageFactory, SixLaborsImageFactory>();
        services.AddSingleton<IFormatDetector, SixLaborsFormatDetector>();
    }
}
