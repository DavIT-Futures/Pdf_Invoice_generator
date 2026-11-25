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
    }
}
