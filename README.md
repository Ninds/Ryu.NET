# Ryu.NET

| master | prerelease | release |
|:------:|:-----:|:--------------:|
|![master](https://github.com/Ninds/Ryu.NET/workflows/Build%20and%20Test/badge.svg) |![prerelease](https://github.com/Ninds/Ryu.NET/workflows/Build%20and%20Test/badge.svg?branch=prerelease) |![release](https://github.com/Ninds/Ryu.NET/workflows/Build%20and%20Test/badge.svg?branch=release)|




[Ryu](https://github.com/ulfjack/ryu) is a high-performance algorithm for converting floats to strings.

This repo was forked from  [RyuCsharp](https://github.com/Dogwei/RyuCsharp) which itself was derived from [Ryu](https://github.com/ulfjack/ryu).

We have added some unit tests generated from the output of Remy Oudompheng's iterator over floating-point numbers which are hard to convert from/to decimal form, [fptest](https://github.com/remyoudompheng/fptest)



## Contents

- [API](#M-API-)
  - [Parse (ReadOnlySpan\<char> s)](#M-RyuDotNet-Ryu-Parse-System-ReadOnlySpan{System-Char}- 'RyuDotNet.Ryu.Parse(System.ReadOnlySpan{System.Char})')
  - [Parse (string s)](#M-RyuDotNet-Ryu-Parse-System-String- 'RyuDotNet.Ryu.Parse(System.String)')
  - [Parse (ReadOnlySpan\<byte> s)](#M-RyuDotNet-Ryu-Parse-System-ReadOnlySpan{System-Byte}- 'RyuDotNet.Ryu.Parse(System.ReadOnlySpan{System.Byte})')
  - [ToStringRyu (double d)](#M-RyuDotNet-Ryu-ToStringRyu-System-Double- 'RyuDotNet.Ryu.ToStringRyu(System.Double)')
  - [ToStringRyu (double d, int Precision)](#M-RyuDotNet-Ryu-ToStringRyu-System-Double,System-Int32- 'RyuDotNet.Ryu.ToStringRyu(System.Double,System.Int32)')
  - [TryParse (ReadOnlySpan\<char>  s, out double result)](#M-RyuDotNet-Ryu-TryParse-System-ReadOnlySpan{System-Char},System-Double@- 'RyuDotNet.Ryu.TryParse(System.ReadOnlySpan{System.Char},System.Double@)')
  - [TryParse (string s, out double result)](#M-RyuDotNet-Ryu-TryParse-System-String,System-Double@- 'RyuDotNet.Ryu.TryParse(System.String,System.Double@)')
  - [TryParse (ReadOnlySpan\<byte> s, out double result)](#M-RyuDotNet-Ryu-TryParse-System-ReadOnlySpan{System-Byte},System-Double@- 'RyuDotNet.Ryu.TryParse(System.ReadOnlySpan{System.Byte}, out double System.Double@)')
  - [WriteTo (double d, Span\<byte> buffer)](#M-RyuDotNet-Ryu-WriteTo-System-Double,System-Span{System-Byte}- 'RyuDotNet.Ryu.WriteTo(System.Double,System.Span{System.Byte})')
  - [WriteTo (double d, Span\<char >buffer)](#M-RyuDotNet-Ryu-WriteTo-System-Double,System-Span{System-Char}- 'RyuDotNet.Ryu.WriteTo(System.Double,System.Span{System.Char})')
  - [WriteTo (double d, int Precision, Span\<byte> buffer)](#M-RyuDotNet-Ryu-WriteTo-System-Double,System-Int32,System-Span{System-Byte}- 'RyuDotNet.Ryu.WriteTo(System.Double,System.Int32,System.Span{System.Byte})')
  - [WriteTo (double d, int Precision, Span\<char> buffer)](#M-RyuDotNet-Ryu-WriteTo-System-Double,System-Int32,System-Span{System-Char}- 'RyuDotNet.Ryu.WriteTo(System.Double,System.Int32,System.Span{System.Char})')
- [BENCHMARKS](#M-RyuDotNet-Ryu-BENHMARK-)


# API
<a name='M-API-'></a>


<a name='M-RyuDotNet-Ryu-Parse-System-ReadOnlySpan{System-Char}-'></a>
## Parse(s) `method`

##### Summary

Converts a character span that contains the string representation of a number  to its double-precision floating-point number equivalent  using
the Ryu algorithm.

##### Returns

A double-precision floating-point number that is equivalent to the numeric value or symbol specified in s.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| s    | [ReadOnlySpan\<char>](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ReadOnlySpan 'System.ReadOnlySpan{System.Char}') | A character span that contains the number to convert. |

<a name='M-RyuDotNet-Ryu-Parse-System-String-'></a>
## Parse(s) `method`

##### Summary

Converts the string representation of a number  to its double-precision floating-point number equivalent using
the Ryu algorithm.

##### Returns

A double-precision floating-point number that is equivalent to the numeric value or symbol specified in s.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| s | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A string containing a number to convert. |

<a name='M-RyuDotNet-Ryu-Parse-System-ReadOnlySpan{System-Byte}-'></a>
## Parse(s) `method`

##### Summary

Converts a byte (ascii) span that contains representation of a number  to its double-precision floating-point number equivalent using
the Ryu algorithm.

##### Returns

A double-precision floating-point number that is equivalent to the numeric value or symbol specified in s.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| s | [ReadOnlySpan\<byte>](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ReadOnlySpan 'System.ReadOnlySpan{System.Byte}') | A byte (ascii) span containing a number to convert. |

<a name='M-RyuDotNet-Ryu-ToStringRyu-System-Double-'></a>
## ToStringRyu(d) `method`

##### Summary

Converts the numeric value of this instance to its equivalent string representation using
the Ryu algorithm.

##### Returns

The string representation of the double value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The double to convert to a string |

<a name='M-RyuDotNet-Ryu-ToStringRyu-System-Double,System-Int32-'></a>
## ToStringRyu(d,Precision) `method`

##### Summary

Converts the numeric value of this instance to its equivalent string representation using
the Ryu algorithm.

##### Returns

The string representation of the double value

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d    | [double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The double to convert to a string |
| Precision | [int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The precision in the  exponential (scientific)notation |

<a name='M-RyuDotNet-Ryu-TryParse-System-ReadOnlySpan{System-Char},System-Double@-'></a>
## TryParse(s,result) `method`

##### Summary

Converts the span representation of a number to its double-precision floating-point number equivalent using
the Ryu algorithm.
A return value indicates whether the conversion succeeded or failed.

##### Returns

true if s was converted successfully; otherwise, false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| s    | [ReadOnlySpan\<char>](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ReadOnlySpan 'System.ReadOnlySpan{System.Char}') | A chacater span  that contains the string representation of the number to convert. |
| result | [out double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double@ 'System.Double@') | When this method returns, contains the double-precision floating-point number equivalent of the numeric value or symbol contained in span parameter,  if the conversion succeeded, or zero if the conversion failed. |

<a name='M-RyuDotNet-Ryu-TryParse-System-String,System-Double@-'></a>
## TryParse(s,result) `method`

##### Summary

Converts the string representation of a number to its double-precision floating-point number equivalent using
the Ryu algorithm.
A return value indicates whether the conversion succeeded or failed.

##### Returns

true if s was converted successfully; otherwise, false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| s | [string](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | A string containing a number to convert. |
| result | [out double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double@ 'System.Double@') | When this method returns, contains the double-precision floating-point number equivalent of the numeric value or symbol contained in span parameter,  if the conversion succeeded, or zero if the conversion failed. |

<a name='M-RyuDotNet-Ryu-TryParse-System-ReadOnlySpan{System-Byte},System-Double@-'></a>
## TryParse(s,result) `method`

##### Summary

Converts the byte (ascii) span containing the string representation of a number to its double-precision floating-point number equivalent using
the Ryu algorithm.
A return value indicates whether the conversion succeeded or failed.

##### Returns

true if s was converted successfully; otherwise, false.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| s | [ReadOnlySpan\<byte>](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ReadOnlySpan 'System.ReadOnlySpan{System.Byte}') | An byte span of the ascii string that contains the string representation of the number to convert. |
| result | [System.Double@](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double@ 'System.Double@') | When this method returns, contains the double-precision floating-point number equivalent of the numeric value or symbol contained in span parameter,  if the conversion succeeded, or zero if the conversion failed. |

<a name='M-RyuDotNet-Ryu-WriteTo-System-Double,System-Span{System-Byte}-'></a>
## WriteTo(d,buffer) `method`

##### Summary

Converts the numeric value of this instance to its equivalent string representation using
the Ryu algorithm.

##### Returns

The ascii (byte) span containing the string representation of the double value which will be a slice of buffer

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The double to convert to a string |
| buffer | [Span\<byte>](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Span 'System.Span{System.Byte}') | A span of bytes in which to place the ascii of the string representation of the double value |

<a name='M-RyuDotNet-Ryu-WriteTo-System-Double,System-Span{System-Char}-'></a>
## WriteTo(d,buffer) `method`

##### Summary

Converts the numeric value of this instance to its equivalent string representation using
the Ryu algorithm.

##### Returns

The characters containing the string representation of the double value which will be a slice of buffer

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The double to convert to a string |
| buffer | [Span\<char>](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Span 'System.Span{System.Char}') | A span of characters in which to place the ascii of the string representation of the double value |

<a name='M-RyuDotNet-Ryu-WriteTo-System-Double,System-Int32,System-Span{System-Byte}-'></a>
## WriteTo(d,Precision,buffer) `method`

##### Summary

Converts the numeric value of this instance to its equivalent string representation using
the Ryu algorithm.

##### Returns

The ascii (byte) span containing the string representation of the double value which will be a slice of buffer

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The double to convert to a string |
| Precision | [int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The precision in the  exponential (scientific)notation |
| buffer | [Span\<byte>](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Span 'System.Span{System.Byte}') | The ascii (byte) span in which to place the ascii of the string representation of the double value |

<a name='M-RyuDotNet-Ryu-WriteTo-System-Double,System-Int32,System-Span{System-Char}-'></a>
## WriteTo(d,Precision,buffer) `method`

##### Summary

Converts the numeric value of this instance to its equivalent string representation using
the Ryu algorithm.

##### Returns

The characters containing the string representation of the double value which will be a slice of buffer

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| d | [double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The double to convert to a string |
| Precision | [int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') | The precision in the  exponential (scientific)notation |
| buffer | [Span\<char>](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Span 'System.Span{System.Char}') | The characters in which to place the ascii of the string representation of the double value |



<a name='M-RyuDotNet-Ryu-BENHMARK-'></a>
# Benchmarks


### Parse a C# (UTF16) string to double 


|         Method |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio |
|--------------- |-------------- |----------:|----------:|----------:|----------:|------:|
|  CSDoubleParse | .NET Core 3.1 | 20.233 ms | 0.0034 ms | 0.1369 ms | 20.233 ms |  1.00 |
| RyuDoubleParse | .NET Core 3.1 |  3.554 ms | 0.0011 ms | 0.0423 ms |  3.541 ms |  0.18 |
|                |               |           |           |           |           |       |
|  CSDoubleParse | .NET Core 5.0 | 10.212 ms | 0.0026 ms | 0.1106 ms | 10.171 ms |  1.00 |
| RyuDoubleParse | .NET Core 5.0 |  3.324 ms | 0.0013 ms | 0.0521 ms |  3.310 ms |  0.33 |


### Parse a string encoded as a sequence of ASCII bytes to double 


|              Method |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio |
|-------------------- |-------------- |----------:|----------:|----------:|----------:|------:|
|  CSDoubleParseAscii | .NET Core 3.1 | 21.405 ms | 0.0037 ms | 0.1507 ms | 21.410 ms |  1.00 |
| RyuDoubleParseAscii | .NET Core 3.1 |  2.232 ms | 0.0011 ms | 0.0434 ms |  2.215 ms |  0.10 |
|                     |               |           |           |           |           |       |
|  CSDoubleParseAscii | .NET Core 5.0 | 11.300 ms | 0.0033 ms | 0.1369 ms | 11.276 ms |  1.00 |
| RyuDoubleParseAscii | .NET Core 5.0 |  2.575 ms | 0.0007 ms | 0.0297 ms |  2.569 ms |  0.23 |


### Print a double as a C# (UTF16) string

|         Method |       Runtime |     Mean |     Error |    StdDev |   Median | Ratio |
|--------------- |-------------- |---------:|----------:|----------:|---------:|------:|
|  CSDoublePrint | .NET Core 3.1 | 7.750 ms | 0.0024 ms | 0.1027 ms | 7.706 ms |  1.00 |
| RyuDoublePrint | .NET Core 3.1 | 3.649 ms | 0.0011 ms | 0.0427 ms | 3.634 ms |  0.47 |
|                |               |          |           |           |          |       |
|  CSDoublePrint | .NET Core 5.0 | 6.907 ms | 0.0022 ms | 0.0945 ms | 6.879 ms |  1.00 |
| RyuDoublePrint | .NET Core 5.0 | 3.418 ms | 0.0010 ms | 0.0403 ms | 3.404 ms |  0.49 |


### Print a double as an ASCII (byte) string

|              Method |       Runtime |     Mean |     Error |    StdDev |   Median | Ratio |
|-------------------- |-------------- |---------:|----------:|----------:|---------:|------:|
|  CSDoublePrintAscii | .NET Core 3.1 | 8.204 ms | 0.0030 ms | 0.1240 ms | 8.151 ms |  1.00 |
| RyuDoublePrintAscii | .NET Core 3.1 | 2.397 ms | 0.0006 ms | 0.0250 ms | 2.388 ms |  0.29 |
|                     |               |          |           |           |          |       |
|  CSDoublePrintAscii | .NET Core 5.0 | 7.469 ms | 0.0024 ms | 0.1001 ms | 7.438 ms |  1.00 |
| RyuDoublePrintAscii | .NET Core 5.0 | 2.308 ms | 0.0005 ms | 0.0213 ms | 2.301 ms |  0.31 |

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.17134.1550 (1803/April2018Update/Redstone4)
Intel Core i7-6700 CPU 3.40GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.100-preview.6.20318.15
  [Host]     : .NET Core 3.1.4 (CoreCLR 4.700.20.20201, CoreFX 4.700.20.22101), X64 RyuJIT
  Job-TPMQGC : .NET Core 3.1.4 (CoreCLR 4.700.20.20201, CoreFX 4.700.20.22101), X64 RyuJIT
  Job-SIYNKC : .NET Core 5.0.0 (CoreCLR 5.0.20.30506, CoreFX 5.0.20.30506), X64 RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=16  
IterationCount=20000  LaunchCount=1  WarmupCount=10  

```