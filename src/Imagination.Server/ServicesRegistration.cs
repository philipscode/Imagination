using System.Diagnostics;
using OpenTelemetry.Context.Propagation;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Imagination;

internal static class ServicesRegistration
{
    internal static readonly ActivitySource Telemetry = new ("Server");

    public static void AddTelemetry(this IServiceCollection services)
    {
        OpenTelemetry.Sdk.SetDefaultTextMapPropagator(new B3Propagator());
        
        services.AddOpenTelemetryTracing(providerBuilder => providerBuilder
            .SetResourceBuilder(ResourceBuilder
                .CreateDefault()
                .AddEnvironmentVariableDetector()
                .AddTelemetrySdk()
                .AddService("Imagination"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddJaegerExporter()
            .AddSource(Telemetry.Name));
    }
}
