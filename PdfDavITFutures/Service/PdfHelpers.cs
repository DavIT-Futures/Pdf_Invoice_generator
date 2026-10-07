using iTextSharp.text;
using System;
using System.IO;
using D = System.Drawing;

namespace PdfDavITFutures.Service
{
    public static class PdfHelpers
    {
        public static string GenerateInvoiceNo(DateTime date, string invoiceNo)
        {
            if (!string.IsNullOrEmpty(invoiceNo))
                return invoiceNo;
            string result = string.Format("{0}{1}{2}-{3}{4}{5}", date.Year.ToString("D4"), date.Month.ToString("D2"), date.Day.ToString("D2"), date.Hour.ToString("D2"), date.Minute.ToString("D2"), date.Second.ToString("D2"));
            return result;
        }

        /// <summary>
        /// Create an iTextSharp.text.Image from a System.Drawing.Image resource in a safe way (uses memory stream).
        /// This avoids relying on image-format-specific overloads and is robust on .NET 8 Windows.
        /// </summary>
        public static Image GetImageFromResource(D.Image image, D.Imaging.ImageFormat format)
        {
            if (image == null) return null;
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                ms.Position = 0;
                return Image.GetInstance(ms);
            }
        }

        public static decimal ParseDecimalSafe(string s, decimal defaultValue = 0m)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;
            if (decimal.TryParse(s, out var v)) return v;
            // try replace comma with dot
            var normalized = s.Replace(',', '.');
            if (decimal.TryParse(normalized, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out v)) return v;
            return defaultValue;
        }

        public static int ParseIntSafe(string s, int defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;
            if (int.TryParse(s, out var v)) return v;
            if (int.TryParse(s.Trim(), out v)) return v;
            return defaultValue;
        }

        public static string GenerateFileName(DateTime date, Company company)
        {
            string y = date.Year.ToString("D4");
            string m = date.Month.ToString("D2");
            string d = date.Day.ToString("D2");
            return company switch
            {
                Company.Tyrecheck => $"{y}-{m}E - Tyrecheck.pdf",
                Company.Tirecheck => $"{y}-{m}E - Tirecheck.pdf",
                Company.ITKontrakt => $"{y}-{m} - ITKontrakt.pdf",
                Company.Intive => $"{y}-{m}-{d} - sale [sprzedaz] - Intive.pdf",
                Company.Cododile => $"{y}-{m}-{d} - sale [sprzedaz] - Cododile.pdf",
                Company.PaulRyan => $"{y}-{m}-{d} - sale [sprzedaz] - Paul Ryan.pdf",
                Company.SII => $"{y}-{m}-{d} - sale [sprzedaz] - SII.pdf",
                Company.Coltech => $"{y}-{m}-{d} - sale [sprzedaz] - Coltech.pdf",
                Company.IgGroup => $"{y}-{m}-{d} - sale [sprzedaz] - IgKnowhow.pdf",
                Company.IgGroupBenefit => $"{y}-{m}-{d} - sale [sprzedaz] - IgKnowhow 2.pdf",
                _ => $"{y}-{m}-{d} - invoice.pdf",
            };
        }
    }
}
