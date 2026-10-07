using System;
using PdfDavITFutures.Service;
using Xunit;

namespace PdfDavITFutures.Tests
{
    public class PdfHelpersParsingTests
    {
        [Theory]
        [InlineData("123.45", 123.45)]
        [InlineData("123,45", 123.45)]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        public void ParseDecimalSafe_ParsesVariousFormats(string input, decimal expected)
        {
            var v = PdfHelpers.ParseDecimalSafe(input);
            Assert.Equal(expected, v);
        }

        [Theory]
        [InlineData("10", 10)]
        [InlineData(" 20 ", 20)]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        public void ParseIntSafe_ParsesVariousFormats(string input, int expected)
        {
            var v = PdfHelpers.ParseIntSafe(input);
            Assert.Equal(expected, v);
        }
    }
}
