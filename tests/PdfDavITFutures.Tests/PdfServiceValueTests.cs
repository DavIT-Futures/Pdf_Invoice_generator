using System;
using PdfDavITFutures.Service;
using Xunit;

namespace PdfDavITFutures.Tests
{
    public class PdfServiceValueTests
    {
        [Fact]
        public void Tirecheck_ComputesValue_AsPerDayTimesDays()
        {
            var doi = new DateTime(2023,1,1);
            var deadline = doi.AddDays(30);
            var svc = new PdfService(doi, deadline, "100", "3", "0", Company.Tirecheck, Currency.EUR, PaymentType.Daily, "", "Service");
            Assert.Equal(100m, svc.PerDay);
            Assert.Equal(3, svc.NumberOfDays);
            Assert.Equal(300m, svc.Value);
        }

        [Fact]
        public void ITKontrakt_ComputesValue_AsPerHourTimesHours()
        {
            var doi = new DateTime(2023,1,1);
            var deadline = doi.AddDays(30);
            var svc = new PdfService(doi, deadline, "50", "0", "4", Company.ITKontrakt, Currency.PLN, PaymentType.Hourly, "", "Service");
            Assert.Equal(50m, svc.PerHour);
            Assert.Equal(4, svc.NumberOfHours);
            Assert.Equal(200m, svc.Value);
        }
    }
}
