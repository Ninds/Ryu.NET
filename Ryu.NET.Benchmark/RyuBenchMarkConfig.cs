using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace Ryu.NET.Benchmark
{
    class RyuBenchMarkConfig : ManualConfig
    {
        public RyuBenchMarkConfig()
        {
            AddJob(Job.Default.
                        WithPlatform(Platform.X64).
                        WithRuntime(CoreRuntime.Core31).
                        WithJit(Jit.RyuJit).
                        WithIterationCount(1000).
                        WithLaunchCount(5).
                        WithWarmupCount(10).
                        WithInvocationCount(16)
                  );

        }
    }
}