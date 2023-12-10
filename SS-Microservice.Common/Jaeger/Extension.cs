using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using SS_Microservice.Common.Configuration;

namespace SS_Microservice.Common.Jaeger
{
    public static class Extension
    {
        private static bool _initialized = false;

        public static IServiceCollection AddJaeger(this IServiceCollection services, JaegerOptions options)
        {
            if (_initialized) return services;

            _initialized = true;
            if (!options.Enabled)
            {
                services.AddSingleton(DefaultTracer.Create());
                return services;
            }
            services.AddSingleton<ITracer>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

                var reporter = new RemoteReporter
                .Builder()
                .WithSender(new UdpSender(options.UdpHost, options.UdpPort, options.MaxPacketSize))
                .WithLoggerFactory(loggerFactory)
                .Build();

                var sampler = GetSampler(options);

                var tracer = new Tracer
                .Builder(options.ServiceName)
                .WithReporter(reporter)
                .WithSampler(sampler)
                .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });
            return services;
        }

        public static JaegerOptions GetJaegerOptions(this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return configuration.GetOptions<JaegerOptions>("jaeger");
        }

        private static ISampler GetSampler(JaegerOptions options)
        {
            switch (options.Sampler)
            {
                case "const": return new ConstSampler(true);
                case "rate": return new RateLimitingSampler(options.MaxTracesPerSecond);
                case "probabilistic": return new ProbabilisticSampler(options.SamplingRate);
                default: return new ConstSampler(true);
            }
        }
    }
}