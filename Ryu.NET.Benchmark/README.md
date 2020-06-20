
Initial (crude) Benchmark of Ryu vs C# for parsing a C# string to double.


``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.900 (1903/May2019Update/19H1)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  Job-IBGFYC : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT

Jit=RyuJit  Platform=X64  Runtime=.NET Core 3.1  
InvocationCount=16  IterationCount=1000  LaunchCount=5  
WarmupCount=10  

```
|         Method |       Mean |   Error |    StdDev |     Median | Ratio |
|--------------- |-----------:|--------:|----------:|-----------:|------:|
|  CSDoubleParse | 4,486.4 μs | 7.61 μs | 160.73 μs | 4,460.0 μs |  1.00 |
| RyuDoubleParse |   499.9 μs | 1.40 μs |  29.61 μs |   497.7 μs |  0.11 |
