
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
|  CSDoubleParse | 4,486.4 µs | 7.61 µs | 160.73 µs | 4,460.0 µs |  1.00 |
| RyuDoubleParse |   499.9 µs | 1.40 µs |  29.61 µs |   497.7 µs |  0.11 |



##### Migrate s2d_n from char* to ReadOnlySpan
After making the switch from `char*` to `ReadOnlySpan<char>`
```csharp
public static Status s2d_n(char* buffer, int len, double* result)
```
to 
```csharp
public static Status s2d_n(ReadOnlySpan<char> buffer, out double result)
```
no impact :

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.900 (1903/May2019Update/19H1)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  Job-SJMHIW : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT

Jit=RyuJit  Platform=X64  Runtime=.NET Core 3.1  
InvocationCount=16  IterationCount=1000  LaunchCount=5  
WarmupCount=10  

```
|         Method |       Mean |   Error |    StdDev |     Median | Ratio |
|--------------- |-----------:|--------:|----------:|-----------:|------:|
|  CSDoubleParse | 4,421.4 μs | 4.92 μs | 104.11 μs | 4,418.3 μs |  1.00 |
| RyuDoubleParse |   479.5 μs | 1.35 μs |  28.59 μs |   476.4 μs |  0.11 |

**NOTE: `Double.Parse(ReadOnlySpan<Char>,..)` is not in .NET Standard 2.0, only in .NET Core**


##### Version of s2d_n that takes a ReadOnlySpan<byte> vs double.Parse(Encoding.ASCII.GetString(..)
*The use case is for parsing doubles encoded as strings of bytes coming over the wire. .NET doesn't have an explicit function and a string must be constructed (hence allocated) from the array of bytes* 

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18362.900 (1903/May2019Update/19H1)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  Job-VJOWQP : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT

Jit=RyuJit  Platform=X64  Runtime=.NET Core 3.1  
InvocationCount=16  IterationCount=1000  LaunchCount=5  
WarmupCount=10  

```
|         Method |       Mean |   Error |    StdDev |     Median | Ratio |
|--------------- |-----------:|--------:|----------:|-----------:|------:|
|  CSDoubleParse | 4,697.4 μs | 5.07 μs | 106.67 μs | 4,692.0 μs |  1.00 |
| RyuDoubleParse |   485.2 μs | 1.29 μs |  27.40 μs |   482.3 μs |  0.10 |
