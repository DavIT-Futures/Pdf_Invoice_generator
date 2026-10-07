using System;
using PdfDavITFutures.Service;
using Xunit;

namespace PdfDavITFutures.Tests
{
    public class GenerateFileNameTests
    {
        [Theory]
        [InlineData(2023, 1, 1, Company.Tyrecheck, "2023-01E - Tyrecheck.pdf")]
        [InlineData(2023, 12, 31, Company.Tirecheck, "2023-12E - Tirecheck.pdf")]
        [InlineData(2020, 2, 5, Company.ITKontrakt, "2020-02 - ITKontrakt.pdf")]
        [InlineData(2021, 3, 7, Company.Intive, "2021-03-07 - sale [sprzedaz] - Intive.pdf")]
        public void GenerateFileName_ReturnsExpected(int year, int month, int day, Company company, string expected)
        {
            var date = new DateTime(year, month, day);
            var res = PdfHelpers.GenerateFileName(date, company);
            Assert.Equal(expected, res);
        }
    }
}
