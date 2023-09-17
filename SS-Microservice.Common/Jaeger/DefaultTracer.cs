using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SS_Microservice.Common.Jaeger
{
    internal class DefaultTracer
    {
        public static ITracer Create()
                => new Tracer.Builder(Assembly.GetEntryAssembly().FullName)
                    .WithReporter(new NoopReporter())
                    .WithSampler(new ConstSampler(false))
                    .Build();
    }
}