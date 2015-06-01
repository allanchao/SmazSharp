using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmazSharpTests
{
    public class Options
    {
        [Option('p', "perf",
            DefaultValue = false,
            HelpText = "Runs compression and decompression performance tests (same as: -c -d).")]
        public bool RunPerformance { get; set; }

        [Option('c', "compression",
            DefaultValue = false,
            HelpText = "Runs compression performance tests")]
        public bool RunPerformanceCompression { get; set; }

        [Option('d', "decompression",
            DefaultValue = false,
            HelpText = "Runs decompression performance tests")]
        public bool RunPerformanceDecompression { get; set; }

        [Option('c', "cycles",
            Required = false, DefaultValue = 10,
            HelpText = "Number of performance cycles to evaluate (with --perf)")]
        public int PerformanceCycles { get; set; }

        [Option('i', "iterations",
            Required = false, DefaultValue = 100000,
            HelpText = "Number of iterations within each performance cycle (with --perf)")]
        public int PerformanceIterations { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(
                this,
                ht => HelpText.DefaultParsingErrorsHandler(this, ht)
            );
        }
    }
}
