using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SS_Microservice.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Metrics
{
    public static class Extension
    {
        private static bool _initialized;

        public static IHostBuilder UseAppMetrics(this IHostBuilder builder, IConfiguration configuration)
        {
            if (_initialized)
            {
                return builder;
            }

            var metricsOptions = configuration.GetOptions<MetricsOptions>("metrics");

            return builder
                .ConfigureMetricsWithDefaults((ctx, b) =>
                {
                    if (!metricsOptions.Enabled)
                    {
                        return;
                    }
                    _initialized = true;

                    b.Configuration.Configure(cfg =>
                    {
                        cfg.AddEnvTag(metricsOptions.Env);
                    });
                })
                .UseMetricsWebTracking()
                .UseMetrics((ctx, option) =>
                {
                    if (!metricsOptions.Enabled || !metricsOptions.PrometheusEnabled)
                    {
                        return;
                    }

                    option.EndpointOptions = endpointOpts =>
                    {
                        endpointOpts.MetricsEndpointOutputFormatter = (metricsOptions.PrometheusFormatter?.ToLowerInvariant() ?? string.Empty) switch
                        {
                            "protobuf" => new MetricsPrometheusProtobufOutputFormatter(),
                            _ => new MetricsPrometheusTextOutputFormatter(),
                        };
                        endpointOpts.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                        endpointOpts.EnvironmentInfoEndpointEnabled = false;
                    };
                });
        }
    }
}