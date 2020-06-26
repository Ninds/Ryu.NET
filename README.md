# Ryu.NET
![.NET Core](https://github.com/Ninds/Ryu.NET/workflows/Build%20and%20Test/badge.svg)

[Ryu](https://github.com/ulfjack/ryu) is a high-performance algorithm for converting floats to strings.

This repo was forked from  [RyuCsharp](https://github.com/Dogwei/RyuCsharp) which itself was derived from [Ryu](https://github.com/ulfjack/ryu).

We have added some unit tests generated from the output of Remy Oudompheng's iterator over floating-point numbers which are hard to convert from/to decimal form, [fptest](https://github.com/remyoudompheng/fptest)


Initial Crude benchmarks


### Parse a C# (UTF16) string to double 


|         Method |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio |
|--------------- |-------------- |----------:|----------:|----------:|----------:|------:|
|  CSDoubleParse | .NET Core 3.1 | 22.857 ms | 0.0289 ms | 0.6206 ms | 22.882 ms |  1.00 |
| RyuDoubleParse | .NET Core 3.1 |  3.046 ms | 0.0051 ms | 0.1083 ms |  3.014 ms |  0.13 |
|                |               |           |           |           |           |       |
|  CSDoubleParse | .NET Core 5.0 | 10.249 ms | 0.0117 ms | 0.2497 ms | 10.291 ms |  1.00 |
| RyuDoubleParse | .NET Core 5.0 |  2.606 ms | 0.0047 ms | 0.1009 ms |  2.580 ms |  0.25 |


### Parse a string encoded as a sequence of ASCII bytes to double 


|              Method |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio |
|-------------------- |-------------- |----------:|----------:|----------:|----------:|------:|
|  CSDoubleParseAscii | .NET Core 3.1 | 23.805 ms | 0.0284 ms | 0.6092 ms | 23.822 ms |  1.00 |
| RyuDoubleParseAscii | .NET Core 3.1 |  2.856 ms | 0.0038 ms | 0.0799 ms |  2.847 ms |  0.12 |
|                     |               |           |           |           |           |       |
|  CSDoubleParseAscii | .NET Core 5.0 | 11.554 ms | 0.0178 ms | 0.3828 ms | 11.561 ms |  1.00 |
| RyuDoubleParseAscii | .NET Core 5.0 |  2.658 ms | 0.0044 ms | 0.0942 ms |  2.635 ms |  0.23 |


### Print a double as a C# (UTF16) string

|         Method |       Runtime |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |
|--------------- |-------------- |---------:|----------:|----------:|---------:|------:|--------:|
|  CSDoublePrint | .NET Core 3.1 | 7.106 ms | 0.0075 ms | 0.1607 ms | 7.111 ms |  1.00 |    0.00 |
| RyuDoublePrint | .NET Core 3.1 | 3.466 ms | 0.0069 ms | 0.1443 ms | 3.430 ms |  0.49 |    0.02 |
|                |               |          |           |           |          |       |         |
|  CSDoublePrint | .NET Core 5.0 | 5.491 ms | 0.0086 ms | 0.1840 ms | 5.498 ms |  1.00 |    0.00 |
| RyuDoublePrint | .NET Core 5.0 | 3.092 ms | 0.0058 ms | 0.1227 ms | 3.060 ms |  0.56 |    0.03 |


### Print a double as an ASCII (byte) string

|              Method |       Runtime |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD |
|-------------------- |-------------- |---------:|----------:|----------:|---------:|------:|--------:|
|  CSDoublePrintAscii | .NET Core 3.1 | 7.637 ms | 0.0091 ms | 0.1937 ms | 7.635 ms |  1.00 |    0.00 |
| RyuDoublePrintAscii | .NET Core 3.1 | 3.603 ms | 0.0094 ms | 0.2011 ms | 3.587 ms |  0.47 |    0.03 |
|                     |               |          |           |           |          |       |         |
|  CSDoublePrintAscii | .NET Core 5.0 | 5.970 ms | 0.0196 ms | 0.4198 ms | 6.072 ms |  1.00 |    0.00 |
| RyuDoublePrintAscii | .NET Core 5.0 | 3.308 ms | 0.0083 ms | 0.1767 ms | 3.282 ms |  0.56 |    0.05 |

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.900 (1903/May2019Update/19H1)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.5.20279.10
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  Job-WCPBTL : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  Job-SWLASU : .NET Core 5.0.0 (CoreCLR 5.0.20.27801, CoreFX 5.0.20.27801), X64 RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=16  
IterationCount=5000  LaunchCount=1  WarmupCount=10  

```