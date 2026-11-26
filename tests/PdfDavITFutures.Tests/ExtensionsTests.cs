using iTextSharp.text;
using PdfDavITFutures;
using Xunit;

namespace PdfDavITFutures.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void ToPdfPoints_OneInchInMillimeters_ReturnsSeventyTwoPoints()
        {
            float points = 25.4f.ToPdfPoints();
            Assert.Equal(72f, points, precision: 3);
        }

        [Fact]
        public void CoordinateHelpers_OffsetFromPageEdges_SubtractFromA4Dimensions()
        {
            Assert.Equal(PageSize.A4.Width, 0f.x(), precision: 3);
            Assert.Equal(PageSize.A4.Height, 0f.y(), precision: 3);
            Assert.Equal(PageSize.A4.Width - 20f, 20f.x(), precision: 3);
            Assert.Equal(PageSize.A4.Height - 35f, 35f.y(), precision: 3);
        }
    }
}
