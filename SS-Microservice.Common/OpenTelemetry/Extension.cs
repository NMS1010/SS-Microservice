using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SS_Microservice.Common.Configuration;
using SS_Microservice.Common.Jaeger;
using System.Diagnostics;

namespace SS_Microservice.Common.OpenTelemetry
{
    public static class Extension
    {
        public static IServiceCollection AddCustomOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
        {
            JaegerOptions jaegerOptions = configuration.GetOptions<JaegerOptions>("jaeger");

            services.AddOpenTelemetry()
                .ConfigureResource(b =>
                {
                    b.AddService(jaegerOptions.ServiceName);
                })
                .WithTracing(provider => provider
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(jaegerOptions.ServiceName))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddSource(new ActivitySource(jaegerOptions.ServiceName).Name)
                    .AddOtlpExporter()
                )
                .WithMetrics(provider => provider
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(jaegerOptions.ServiceName))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddPrometheusExporter()
                );

            return services;
        }
    }
}
