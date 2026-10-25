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
        [InlineData(2022, 5, 10, Company.Cododile, "2022-05-10 - sale [sprzedaz] - Cododile.pdf")]
        [InlineData(2022, 6, 15, Company.PaulRyan, "2022-06-15 - sale [sprzedaz] - Paul Ryan.pdf")]
        [InlineData(2023, 8, 20, Company.SII, "2023-08-20 - sale [sprzedaz] - SII.pdf")]
        [InlineData(2023, 9, 25, Company.Coltech, "2023-09-25 - sale [sprzedaz] - Coltech.pdf")]
        [InlineData(2024, 10, 30, Company.IgGroup, "2024-10-30 - sale [sprzedaz] - IgKnowhow.pdf")]
        [InlineData(2024, 11, 1, Company.IgGroupBenefit, "2024-11-01 - sale [sprzedaz] - IgKnowhow 2.pdf")]
        public void GenerateFileName_ReturnsExpected(int year, int month, int day, Company company, string expected)
        {
            var date = new DateTime(year, month, day);
            var res = PdfHelpers.GenerateFileName(date, company);
            Assert.Equal(expected, res);
        }
    }
}
