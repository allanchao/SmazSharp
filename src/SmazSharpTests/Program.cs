using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static System.Console;

namespace SmazSharpTests
{
    static class Program
    {
        /// <summary>
        /// Entry point for stand-alone running
        /// </summary>
        static void Main(string[] args)
        {
            var options = new Options();

            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                try
                {
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine("SmazSharp Tests");
                    WriteLine("Copyright ©  2006 - 2009, Salvatore Sanfilippo");
                    WriteLine("This is free software.You may redistribute copies of it under the terms of");
                    WriteLine("the MIT License <http://www.opensource.org/licenses/mit-license.php>");
                    WriteLine();

                    // Perform Compliance Testing
                    var complianceTests = new ComplianceTests();

                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("------");
                    WriteLine("Running Compliance Tests");

                    WriteLine("  Testing Compression");
                    ForegroundColor = ConsoleColor.Gray;
                    complianceTests.CompressTest();
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("    Test Passed");

                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("  Testing Decompression");
                    ForegroundColor = ConsoleColor.Gray;
                    complianceTests.DecompressTest();
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("    Test Passed");

                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("  Testing Random Compression->Decompression");
                    ForegroundColor = ConsoleColor.Gray;
                    complianceTests.RandomTest();
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("    Test Passed                      ");

                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("Compliance Testing Completed");
                    WriteLine("------");
                    WriteLine();
                    ForegroundColor = ConsoleColor.Gray;

                    if (options.RunPerformance || options.RunPerformanceCompression)
                    {
                        var performanceTests = new PerformanceTests();

                        performanceTests.CompressPerfTest(options.PerformanceCycles, options.PerformanceIterations);
                    }

                    if (options.RunPerformance || options.RunPerformanceDecompression)
                    {
                        var performanceTests = new PerformanceTests();

                        performanceTests.DecompressPerfTest(options.PerformanceCycles, options.PerformanceIterations);
                    }
                }
                catch (UnitTestAssertException ex)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                }
            }
        }
    }
}
