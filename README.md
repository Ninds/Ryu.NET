# Ryu.NET

[Ryu](https://github.com/ulfjack/ryu) is a high-performance algorithm for converting floats to strings.

This repo was forked from  [RyuCsharp](https://github.com/Dogwei/RyuCsharp) which itself was derived from [Ryu](https://github.com/ulfjack/ryu).

We have added some unit tests generated from the output from Remy Oudompheng's iterator over floating-point numbers which are hard to convert from/to decimal form. [fptest](https://github.com/remyoudompheng/fptest)


Initial Crude benchmarks


### Parse a C# (UTF16) string to double 
```csharp
        [Benchmark(Baseline = true)]
        public void CSDoubleParse()
        {
            for (int i = 0; i < SmallTestSet.TestArray.Length; ++i)
            {
                double x = double.Parse(SmallTestSet.TestArray[i],culture);
                x = x + 1.0;
            }
        }

        [Benchmark]
        unsafe public void RyuDoubleParse()
        {
            for (int i = 0; i < SmallTestSet.TestArray.Length; ++i)
            {
                double x;
                Ryu.Net.Internal.Ryu.s2d_n(SmallTestSet.TestArray[i].AsSpan(), out x);
                x = x + 1.0;
            }
        }
```

|         Method |       Runtime |       Mean |   Error |    StdDev |     Median | Ratio | RatioSD |
|--------------- |-------------- |-----------:|--------:|----------:|-----------:|------:|--------:|
|  CSDoubleParse |    .NET 4.7.2 |   999.4 μs | 2.56 μs |  53.30 μs |   979.2 μs |  1.00 |    0.00 |
| RyuDoubleParse |    .NET 4.7.2 |   500.9 μs | 1.00 μs |  19.85 μs |   499.4 μs |  0.50 |    0.03 |
|                |               |            |         |           |            |       |         |
|  CSDoubleParse | .NET Core 3.1 | 4,255.3 μs | 7.00 μs | 148.05 μs | 4,239.5 μs |  1.00 |    0.00 |
| RyuDoubleParse | .NET Core 3.1 |   442.0 μs | 0.87 μs |  17.48 μs |   442.9 μs |  0.10 |    0.01 |
|                |               |            |         |           |            |       |         |
|  CSDoubleParse | .NET Core 5.0 | 2,081.8 μs | 4.70 μs |  99.43 μs | 2,041.6 μs |  1.00 |    0.00 |
| RyuDoubleParse | .NET Core 5.0 |   445.6 μs | 0.83 μs |  16.56 μs |   446.7 μs |  0.21 |    0.01 |


### Parse a string encoded as a sequence of ASCII bytes to double 
```csharp
        byte[][] TestArray;

        [GlobalSetup]
        public void GlobalSetup()
         => TestArray = SmallTestSet.TestAsciiArray;

        [Benchmark(Baseline = true)]
        public void CSDoubleParseAscii()
        {
            for (int i = 0; i < TestArray.Length; ++i)
            {
                double x = double.Parse(Encoding.ASCII.GetString(TestArray[i]),culture);
                x = x + 1.0;
            }
        }

        [Benchmark]
        unsafe public void RyuDoubleParseAscii()
        {
            for (int i = 0; i < TestArray.Length; ++i)
            {
                double x;
                Ryu.Net.Internal.Ryu.s2d_n(TestArray[i], out x);
                x = x + 1.0;
            }
        }

```

|              Method |       Runtime |       Mean |   Error |    StdDev |     Median | Ratio | RatioSD |
|-------------------- |-------------- |-----------:|--------:|----------:|-----------:|------:|--------:|
|  CSDoubleParseAscii |    .NET 4.7.2 | 1,375.7 μs | 2.66 μs |  55.08 μs | 1,367.2 μs |  1.00 |    0.00 |
| RyuDoubleParseAscii |    .NET 4.7.2 |   562.2 μs | 1.22 μs |  25.05 μs |   559.9 μs |  0.41 |    0.02 |
|                     |               |            |         |           |            |       |         |
|  CSDoubleParseAscii | .NET Core 3.1 | 4,829.2 μs | 8.11 μs | 173.23 μs | 4,825.1 μs |  1.00 |    0.00 |
| RyuDoubleParseAscii | .NET Core 3.1 |   471.6 μs | 1.13 μs |  23.40 μs |   468.4 μs |  0.10 |    0.01 |
|                     |               |            |         |           |            |       |         |
|  CSDoubleParseAscii | .NET Core 5.0 | 2,296.7 μs | 4.73 μs |  99.56 μs | 2,264.5 μs |  1.00 |    0.00 |
| RyuDoubleParseAscii | .NET Core 5.0 |   420.5 μs | 0.82 μs |  16.36 μs |   422.2 μs |  0.18 |    0.01 |

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.900 (1903/May2019Update/19H1)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.5.20279.10
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  Job-QAEZTJ : .NET Framework 4.8 (4.8.4180.0), X64 RyuJIT
  Job-VZKZNC : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  Job-WGKNHP : .NET Core 5.0.0 (CoreCLR 5.0.20.27801, CoreFX 5.0.20.27801), X64 RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=16  
IterationCount=5000  LaunchCount=1  WarmupCount=10  

```