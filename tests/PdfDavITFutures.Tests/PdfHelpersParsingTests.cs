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

        [Theory]
        [InlineData("not-a-number", 99.5)]
        [InlineData("   ", -1.25)]
        [InlineData(null, 42.0)]
        public void ParseDecimalSafe_InvalidOrBlankInput_ReturnsCustomDefaultValue(string input, decimal fallback)
        {
            var actual = PdfHelpers.ParseDecimalSafe(input, fallback);
            Assert.Equal(fallback, actual);
        }

        [Theory]
        [InlineData("abc", 7)]
        [InlineData("   ", -3)]
        [InlineData("99999999999999999999", 15)]
        public void ParseIntSafe_InvalidOrOverflowInput_ReturnsCustomDefaultValue(string input, int fallback)
        {
            var actual = PdfHelpers.ParseIntSafe(input, fallback);
            Assert.Equal(fallback, actual);
        }

        [Fact]
        public void GetImageFromResource_WhenImageIsNull_ReturnsNull()
        {
            var result = PdfHelpers.GetImageFromResource(null, System.Drawing.Imaging.ImageFormat.Png);
            Assert.Null(result);
        }
    }
}
