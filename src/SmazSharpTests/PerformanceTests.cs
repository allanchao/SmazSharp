using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmazSharp;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static System.Console;

namespace SmazSharpTests
{
    public class PerformanceTests
    {

        /// <summary>
        /// Test SmazSharp Compression Performance, assume compliance (via other tests)
        /// </summary>
        public void CompressPerfTest(int Cycles, int Iterations)
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("------");
            WriteLine("Testing Compression Performance");
            ForegroundColor = ConsoleColor.Gray;
            WriteLine($"       Cycles: {Cycles:N0}");
            WriteLine($"   Iterations: {Iterations:N0}");

#if DEBUG
            Assert.Inconclusive("Cannot run performance tests in Debug");
#endif
            Assert.IsTrue(Cycles > 0, "The number of Cycles must be greater than 0");
            Assert.IsTrue(Iterations > 0, "The number of Iterations must be greater than 0");

            ForegroundColor = ConsoleColor.Green;
            Write("  Running Tests: 0 %");

            var testBytes = Resources.TestStrings.Select(s => Encoding.ASCII.GetBytes(s)).ToArray();
            long[] results = new long[Cycles];
            byte[] result;

            // Warm Up
            for (int i = 0; i < testBytes.Length; i++)
            {
                result = Smaz.Compress(testBytes[i]);
            }

            // Run Cycles
            for (int cycle = 0; cycle < Cycles; cycle++)
            {
                // Run Iterations
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int iteration = 0; iteration < Iterations; iteration++)
                {
                    Smaz.Decompress(testBytes[iteration % testBytes.Length]);
                }
                sw.Stop();
                results[cycle] = sw.ElapsedTicks;
                SetCursorPosition(17, CursorTop);
                Write($"{(100 / (double)Cycles) * (cycle + 1):N0} %");
            }

            SetCursorPosition(0, CursorTop);
            WriteLine("  Tests Complete       ");
            WriteLine($"    Cycles (ms): {string.Join(", ", results.Select(r => (r / TimeSpan.TicksPerMillisecond).ToString("N1")))}");
            WriteLine($"  Cycle Average: {results.Average() / TimeSpan.TicksPerMillisecond:N2} milliseconds");
            WriteLine($"        Average: {results.Average() / Iterations:N5} ticks/operation");
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Compression Performance Testing Complete");
            WriteLine("------");
            ForegroundColor = ConsoleColor.Gray;
            WriteLine();
        }

        /// <summary>
        /// Test SmazSharp Decompression Performance, assume compliance (via other tests)
        /// </summary>
        public void DecompressPerfTest(int Cycles, int Iterations)
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("------");
            WriteLine("Testing Decompression Performance");
            ForegroundColor = ConsoleColor.Gray;
            WriteLine($"       Cycles: {Cycles:N0}");
            WriteLine($"   Iterations: {Iterations:N0}");

#if DEBUG
            Assert.Inconclusive("Cannot run performance tests in Debug");
#endif
            Assert.IsTrue(Cycles > 0, "The number of Cycles must be greater than 0");
            Assert.IsTrue(Iterations > 0, "The number of Iterations must be greater than 0");

            ForegroundColor = ConsoleColor.Green;
            Write("  Running Tests: 0 %");

            var testBytes = Resources.TestStringsCompressed;
            long[] results = new long[Cycles];
            string result;

            // Warm Up
            for (int i = 0; i < testBytes.Length; i++)
            {
                result = Smaz.Decompress(testBytes[i]);
            }

            // Run Cycles
            for (int cycle = 0; cycle < Cycles; cycle++)
            {
                // Run Iterations
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int iteration = 0; iteration < Iterations; iteration++)
                {
                    Smaz.Decompress(testBytes[iteration % testBytes.Length]);
                }
                sw.Stop();
                results[cycle] = sw.ElapsedTicks;
                SetCursorPosition(17, CursorTop);
                Write($"{(100 / (double)Cycles) * (cycle + 1):N0} %");
            }

            SetCursorPosition(0, CursorTop);
            WriteLine("  Tests Complete       ");
            WriteLine($"    Cycles (ms): {string.Join(", ", results.Select(r => (r / TimeSpan.TicksPerMillisecond).ToString("N1")))}");
            WriteLine($"  Cycle Average: {results.Average() / TimeSpan.TicksPerMillisecond:N2} milliseconds");
            WriteLine($"        Average: {results.Average() / Iterations:N5} ticks/operation");
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Decompression Performance Testing Complete");
            WriteLine("------");
            ForegroundColor = ConsoleColor.Gray;
            WriteLine();
        }

    }
}
