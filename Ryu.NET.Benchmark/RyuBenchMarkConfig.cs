using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace RyuDotNet.Benchmark
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
                        WithLaunchCount(1).
                        WithWarmupCount(10).
                        WithInvocationCount(16)
                  ).
            AddJob(Job.Default.
                      WithPlatform(Platform.X64).
                      WithRuntime(CoreRuntime.Core50).
                      WithJit(Jit.RyuJit).
                      WithIterationCount(1000).
                      WithLaunchCount(1).
                      WithWarmupCount(10).
                      WithInvocationCount(16)
                );//.
            //AddJob(Job.Default.
            //            WithPlatform(Platform.X64).
            //            WithRuntime(ClrRuntime.Net472).
            //            WithJit(Jit.RyuJit).
            //            WithIterationCount(5000).
            //            WithLaunchCount(1).
            //            WithWarmupCount(10).
            //            WithInvocationCount(16)
            //       );

        }
    }
}