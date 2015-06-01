using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmazSharp;
using System;
using System.Text;
using static System.Console;

namespace SmazSharpTests
{
    [TestClass()]
    public class ComplianceTests
    {
        /// <summary>
        /// Tests SmazSharp Compression (against original C implementation)
        /// </summary>
        [TestMethod(), TestCategory("Compliance")]
        public void CompressTest()
        {
            for (int i = 0; i < Resources.TestStrings.Length; i++)
            {
                var test = Resources.TestStrings[i];
                var compressedOk = Resources.TestStringsCompressed[i];
                var compressedTest = Smaz.Compress(test);

                // Compare Length
                if (compressedOk.Length != compressedTest.Length)
                    Assert.Fail("Compression failed for: '{0}'", test);

                // Compare Data
                for (int c = 0; c < compressedOk.Length; c++)
                {
                    if (compressedOk[c] != compressedTest[c])
                        Assert.Fail("Compression failed for: '{0}'", test);
                }
            }
        }

        /// <summary>
        /// Test SmazSharp Decompression (against original C implementation)
        /// </summary>
        [TestMethod(), TestCategory("Compliance")]
        public void DecompressTest()
        {
            for (int i = 0; i < Resources.TestStringsCompressed.Length; i++)
            {
                var test = Resources.TestStringsCompressed[i];
                var decompressedOk = Resources.TestStrings[i];
                var decompressedTest = Smaz.Decompress(test);

                Assert.AreEqual(decompressedOk, decompressedTest, "Decompression failed for: '{0}'", decompressedOk);
            }
        }

        /// <summary>
        /// Tests that 100 random strings compress/decompress successfully
        /// </summary>
        [TestMethod(), TestCategory("Compliance")]
        public void RandomTest()
        {
            var charset = Resources.TestCharSet;
            var test = new StringBuilder(512);
            var random = new Random();

            ForegroundColor = ConsoleColor.Green;
            Write("    Running Test:   0 %");

            for (int cycle = 0; cycle < 10; cycle++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    test.Clear();
                    var length = random.Next(512);

                    while (length-- > 0)
                    {
                        test.Append(charset, random.Next(charset.Length), 1);
                    }

                    var compressed = Smaz.Compress(test.ToString());
                    var decompressed = Smaz.Decompress(compressed);

                    Assert.AreEqual(test.ToString(), decompressed, "Random failed for: '{0}'", test);
                }
                SetCursorPosition(19, CursorTop);
                Write($"{(cycle + 1) * 10:###} %");
            }
            SetCursorPosition(0, CursorTop);
        }
    }
}