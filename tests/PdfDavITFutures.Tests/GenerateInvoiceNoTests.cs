using System;
using PdfDavITFutures.Service;
using Xunit;

namespace PdfDavITFutures.Tests
{
    public class GenerateInvoiceNoTests
    {
        [Fact]
        public void ReturnsProvidedInvoiceNo_WhenNotEmpty()
        {
            var date = new DateTime(2020,1,2,3,4,5);
            var result = PdfHelpers.GenerateInvoiceNo(date, "INV-123");
            Assert.Equal("INV-123", result);
        }

        [Fact]
        public void GeneratesTimestamp_WhenInvoiceNoEmpty()
        {
            var date = new DateTime(2020,12,31,23,59,59);
            var result = PdfHelpers.GenerateInvoiceNo(date, null);
            Assert.Equal("20201231-235959", result);
        }

        [Fact]
        public void GeneratesZeroPaddedTimestamp_WhenInvoiceNoIsEmptyString()
        {
            var date = new DateTime(2026, 3, 5, 7, 8, 9);
            var result = PdfHelpers.GenerateInvoiceNo(date, string.Empty);
            Assert.Equal("20260305-070809", result);
        }
    }
}
