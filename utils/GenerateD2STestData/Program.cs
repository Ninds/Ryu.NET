using System;
using System.IO;
using System.Linq;

namespace GenerateTestData
{
    class Program
    {
        // This program takes as input the output of 
        // https://raw.githubusercontent.com/remyoudompheng/fptest/master/fptest.py
        // and parses it generating data for the tests


        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("usage : <input file> <out dir>");
                    return;
                }
                var inputFilename = args[0];
                var outDir = args[1];
                var dictName = Path.GetFileNameWithoutExtension(inputFilename);

                Directory.CreateDirectory(outDir);

                using (var inputFile = new StreamReader(inputFilename))
                {
                    int count = 0;
                    while (!inputFile.EndOfStream)
                    {
                        using (var outputFile = new StreamWriter(Path.Combine(outDir, dictName + $"_{count}.cs")))
                        {
                            outputFile.WriteLine(
        @$"using System.Collections.Generic;

namespace RyuDotNet.UnitTests.s2d_data
{{
     class {dictName}_{count} : IFPTestData_D2S
     {{   
            public  double[] TestArray =>  _TestArray;
            static  double[] _TestArray =  new double[]
            {{
");
                            int lineNumber = 0;
                            string line;
                            while ((line = inputFile.ReadLine()) != null && lineNumber < 5000)
                            {
                                var tokens = line.Split(null).Where(s => !string.IsNullOrEmpty(s)).ToArray();
                                var theDouble = tokens[1];
                                outputFile.WriteLine(@$"                 {theDouble},");
                                ++lineNumber;
                            }
                            outputFile.WriteLine(
        @$"
            }};
    }}
}}
             
");

                        }
                        ++count;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    }
}
