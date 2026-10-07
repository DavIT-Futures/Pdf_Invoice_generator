using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using D = System.Drawing;

namespace PdfDavITFutures.Service
{
    public enum Company
    {
        Tyrecheck,
        Tirecheck,
        ITKontrakt,
        Intive,
        Cododile,
        PaulRyan,
        SII,
        Coltech,
        IgGroup,
        IgGroupBenefit
    }

    public enum PaymentType
    {
        Daily,
        Hourly,
        Single
    }

    public enum Currency
    {
        EUR,
        PLN,
        GBP
    }

    public class PdfService
    {
        Document _doc;
        PdfWriter _writer;
        System.IO.FileStream _fileStream;
        PdfContentByte _canvas;
        Rectangle _size;
        float _w;
        float _leftMargin;
        float _rightMargin;
        float _leftMargin160;
        float _leftMargin190;
        string _decimalMask;
        string _file;

        public DateTime DateOfIssue { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Value { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfHours { get; set; }
        public decimal PerDay { get; set; }
        public decimal PerHour { get; set; }
        public Company Company { get; set; }
        public Currency Currency { get; set; }
        public PaymentType PaymentType { get; set; }
        public string TxtInvoiceNo { get; private set; }
        public string ServiceName { get; private set; }

        public PdfService(DateTime dateOfIssue, DateTime deadline, string value, string numberofDays, string numberOfHours, Company? company, Currency currency, PaymentType paymentType, string txtInvoiceNo, string serviceName)
        {
            TimeSpan ts = DateTime.Now.TimeOfDay;
            this.DateOfIssue = dateOfIssue.AddHours(ts.Hours).AddMinutes(ts.Minutes).AddSeconds(ts.Seconds);
            this.Deadline = deadline;
            this.Company = company.Value;
            this.Currency = currency;
            this.PaymentType = paymentType;
            this.TxtInvoiceNo = txtInvoiceNo;
            this.ServiceName = serviceName;

            if (company == Company.Tyrecheck)
            {
                this.Value = PdfHelpers.ParseDecimalSafe(value);
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.Tyrecheck));
            }
            else if (company == Company.Tirecheck)
            {
                this.PerDay = PdfHelpers.ParseDecimalSafe(value);
                this.NumberOfDays = PdfHelpers.ParseIntSafe(numberofDays);
                this.Value = this.PerDay * this.NumberOfDays;
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.Tirecheck));
            }
            else if (company == Company.ITKontrakt)
            {
                this.PerHour = PdfHelpers.ParseDecimalSafe(value);
                this.NumberOfHours = PdfHelpers.ParseIntSafe(numberOfHours);
                this.Value = this.PerHour * this.NumberOfHours;
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.ITKontrakt));
            }
            else if (company == Company.Intive)
            {
                this.PerHour = PdfHelpers.ParseDecimalSafe(value);
                this.NumberOfHours = PdfHelpers.ParseIntSafe(numberOfHours);
                this.Value = this.PerHour * this.NumberOfHours;
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.Intive));
            }
            else if (company == Company.Cododile)
            {
                this.PerHour = PdfHelpers.ParseDecimalSafe(value);
                this.NumberOfHours = PdfHelpers.ParseIntSafe(numberOfHours);
                this.Value = this.PerHour * this.NumberOfHours;
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.Cododile));
            }
            else if (company == Company.PaulRyan)
            {
                this.PerHour = PdfHelpers.ParseDecimalSafe(value);
                this.NumberOfHours = PdfHelpers.ParseIntSafe(numberOfHours);
                this.Value = this.PerHour * this.NumberOfHours;
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.PaulRyan));
            }
            else if (company == Company.SII)
            {
                this.PerHour = PdfHelpers.ParseDecimalSafe(value);
                this.NumberOfHours = PdfHelpers.ParseIntSafe(numberOfHours);
                this.Value = this.PerHour * this.NumberOfHours;
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.SII));
            }
            else if (company == Company.Coltech)
            {
                this.PerDay = PdfHelpers.ParseDecimalSafe(value);
                this.NumberOfDays = PdfHelpers.ParseIntSafe(numberofDays);
                this.Value = this.PerDay * this.NumberOfDays;
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.Coltech));
            }
            else if (company == Company.IgGroup)
            {
                this.PerHour = PdfHelpers.ParseDecimalSafe(value);
                this.NumberOfHours = PdfHelpers.ParseIntSafe(numberOfHours);
                this.Value = this.PerHour * this.NumberOfHours;
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.IgGroup));
            }
            else if (company == Company.IgGroupBenefit)
            {
                this.PerHour = PdfHelpers.ParseDecimalSafe(value);
                this.NumberOfHours = PdfHelpers.ParseIntSafe(numberOfHours);
                this.Value = this.PerHour * this.NumberOfHours;
                var appDir = System.AppContext.BaseDirectory;
                _file = System.IO.Path.Combine(appDir, PdfHelpers.GenerateFileName(dateOfIssue, Company.IgGroupBenefit));
            }
        }

        public void GeneratePdf()
        {
            //document setup
            Debug.WriteLine($"[PdfService] Starting GeneratePdf, target file={_file}");
            System.Diagnostics.Trace.WriteLine($"[PdfService] Starting GeneratePdf, target file={_file}");
            try
            {
                Setup();

                //data of iss
            SetText(x(350), y(15), 8f, "City, date of issue [data wystawienia]:");
            SetText(x(505), y(15), 8f, DateOfIssue.ToShortDateString());

            SetText(x(395), y(24), 8f, "date of sale [data sprzedaży]:");
            SetText(x(505), y(24), 8f, DateOfIssue.ToShortDateString());

            //seller
            Seller();

            //logo
            var resImg = Properties.Resources.DavIT_Futures2;
            Image img = PdfHelpers.GetImageFromResource(resImg, D.Imaging.ImageFormat.Jpeg);
            if (img != null)
            {
                _canvas.AddImage(img, img.Width / 8, 0, 0, img.Height / 8, x(380), y(140));
                _canvas.Stroke();
            }
            _canvas.Stroke();

            //buyer
            Buyer();

            //bar - invoice header
            Bar(_leftMargin, y(310), _rightMargin, y(280), 30, "Invoice no [Faktura nr]: " + PdfHelpers.GenerateInvoiceNo(DateOfIssue, this.TxtInvoiceNo));

            //main table header
            PdfPTable mainTable = MainTableHeader();
            mainTable = MainTableAddValues(mainTable, Value, ServiceName);
            mainTable = MainTableAddSummary(mainTable, Value);
            mainTable.WriteSelectedRows(0, -1, _leftMargin, y(320), _canvas);
            _canvas.Stroke();

            //VAT table
            PdfPTable vatTable = VatTableHeader();
            vatTable = VatTableAddValues(vatTable, Value);
            vatTable.WriteSelectedRows(0, -1, _leftMargin + (_rightMargin - _leftMargin) / 2, y(440), _canvas);
            _canvas.Stroke();

            //bar - summary
            Bar(_leftMargin, y(560), _rightMargin, y(530), 30, "  Total amount [Naleznosc ogolem]: " + Value.ToString(_decimalMask) + " " + this.Currency, Element.ALIGN_LEFT);

            //summary
            Summary();

            //signatures
            Signature(_leftMargin + 30, y(710), "Invoice receiver [podpis osoby uprawnionej do odbioru faktury]");
            Signature(_rightMargin - 30 - _w / 3, y(710), "Invoice Issuer [podpis osoby uprawnionej do wystawienia faktury]");

            //footer
            Divider(y(800));
            SetText(x(490), y(808), 9f, "Page [Strona] 1/1");

                _doc.Close();
                // Ensure writer and underlying file stream are closed. Do not open the PDF automatically.
                try
                {
                    _writer?.Close();
                }
                catch { }
                try
                {
                    _fileStream?.Dispose();
                }
                catch { }

                Debug.WriteLine($"[PdfService] PDF generation finished: {_file}");
                System.Diagnostics.Trace.WriteLine($"[PdfService] PDF generation finished: {_file}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[PdfService] GeneratePdf failed: {ex}");
                System.Diagnostics.Trace.WriteLine($"[PdfService] GeneratePdf failed: {ex}");
                throw;
            }
        }

        /// <summary>
        /// Expose generated file path for UI/logging
        /// </summary>
        public string GeneratedFilePath => _file;

        private void Summary()
        {
            int dif = 12;
            int yy = 575 - dif;
            float textSize = 9;

            SetText(_leftMargin, y(yy += dif), textSize, @"Amount to pay [Pozostaje do zaplaty]:");
            SetText(_leftMargin, y(yy += dif), textSize, @"Payment type [Platnosc]:");
            SetText(_leftMargin, y(yy += dif), textSize, @"Due date [Termin zaplaty do]:");

            yy = 575 - dif;
            SetText(_leftMargin190, y(yy += dif), textSize, Value.ToString(_decimalMask) + " " + this.Currency);
            SetText(_leftMargin190, y(yy += dif), textSize, @"Bank transfer [przelew]");
            SetText(_leftMargin190, y(yy += dif), textSize, Deadline.ToShortDateString());
        }

        private void Buyer()
        {
            var tempY = 200;
            SetText(_leftMargin, y(tempY), 8f, "Customer [Nabywca]:");
            Divider(y(tempY + 2));
            int dif = 12;
            int yy = tempY + 15 - dif;
            float textSize = 9;

            SetTextBold(_leftMargin, y(yy += dif), textSize, @"Name [Nazwa]:");
            if (Company != Company.PaulRyan && Company != Company.Coltech && Company != Company.IgGroup && Company != Company.IgGroupBenefit)
            {
                SetText(_leftMargin, y(yy += dif), textSize, @"");
                SetText(_leftMargin, y(yy += dif), textSize, @"");
                SetText(_leftMargin, y(yy += dif), textSize, @"");
                SetTextBold(_leftMargin, y(yy += dif), textSize, @"Correspondence address:");
                SetTextBold(_leftMargin, y(yy += dif), textSize, @"[Adres korespondencyjny]:");
            }

            yy = tempY + 15 - dif;
            if (Company == Company.Tyrecheck)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"Tyrecheck");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Information Age Park, Gort Road");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Suite 23");
                SetText(_leftMargin160, y(yy += dif), textSize, @"IE 6415132");
            }
            else if (Company == Company.Tirecheck)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"Tirecheck s.r.o.");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Jindrisska 937/16");
                SetText(_leftMargin160, y(yy += dif), textSize, @"11000 Praha 1");
                SetText(_leftMargin160, y(yy += dif), textSize, @"CZ 03104915");
            }
            else if (Company == Company.ITKontrakt)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"IT KONTRAKT");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Gwiaździsta 66");
                SetText(_leftMargin160, y(yy += dif), textSize, @"53-413 Wrocław");
                SetText(_leftMargin160, y(yy += dif), textSize, @"NIP: 899-25-09-595, KRS: 0000210937, REGON: 933006411");
            }
            else if (Company == Company.Intive)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"intive GmbH");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Franz – Mayer Str. 5");
                SetText(_leftMargin160, y(yy += dif), textSize, @"93053 Regensburg, Niemcy");
                SetText(_leftMargin160, y(yy += dif), textSize, @"NIP: PL1070043765");

                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"intive GmbH Spółka z o.o.Oddział w Polsce");
                SetText(_leftMargin160, y(yy += dif), textSize, @"ul. 1 Sierpnia 8");
                SetText(_leftMargin160, y(yy += dif), textSize, @"02-134 Warszawa");
            }
            else if (Company == Company.Cododile)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"Cododile sp. z o.o.");
                SetText(_leftMargin160, y(yy += dif), textSize, @"ul. Marszałka Józefa Piłsudzkiego 74/320");
                SetText(_leftMargin160, y(yy += dif), textSize, @"50-020 Wrocław");
                SetText(_leftMargin160, y(yy += dif), textSize, @"NIP: 8971873025, KRS: 0000814011");
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"Cododile sp. z o.o.");
                SetText(_leftMargin160, y(yy += dif), textSize, @"ul. Marszałka Józefa Piłsudzkiego 74/320");
                SetText(_leftMargin160, y(yy += dif), textSize, @"50-020 Wrocław");
            }
            else if (Company == Company.PaulRyan)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"Paul Ryan");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Falkirk");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Scotland");
                SetText(_leftMargin160, y(yy += dif), textSize, @"United Kingdom");
            }
            else if (Company == Company.SII)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"SII Sp. z o.o.");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Al. Niepodległości 69");
                SetText(_leftMargin160, y(yy += dif), textSize, @"02-626 Warszawa");
                SetText(_leftMargin160, y(yy += dif), textSize, @"NIP: 5252352907");
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"SII Sp. z o.o.");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Al. Niepodległości 69");
                SetText(_leftMargin160, y(yy += dif), textSize, @"02-626 Warszawa");
            }
            else if (Company == Company.Coltech)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"Coltech Recruitment Ltd");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Dawson House, 5 Jewry Street");
                SetText(_leftMargin160, y(yy += dif), textSize, @"London, United Kingdom, EC3N 2EX");
                SetText(_leftMargin160, y(yy += dif), textSize, @"VAT Number: 313505540");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Company Reg: 11743993");
            }
            else if (Company == Company.IgGroup || Company == Company.IgGroupBenefit)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"IG Knowhow Limited");
                SetText(_leftMargin160, y(yy += dif), textSize, @"Cannon Bridge House");
                SetText(_leftMargin160, y(yy += dif), textSize, @"25 Dowgate Hill");
                SetText(_leftMargin160, y(yy += dif), textSize, @"EC4R 2YA London, United Kingdom");
                SetText(_leftMargin160, y(yy += dif), textSize, @"NIP: 106-000-51-31");
            }
        }

        private PdfPTable VatTableAddValues(PdfPTable vatTable, decimal value)
        {
            if (Company == Company.PaulRyan || Company == Company.Coltech)
            {
                vatTable.AddCell(CreateCell(value.ToString(_decimalMask)));
                vatTable.AddCell(CreateCell("n/a [np]"));
                decimal calculatedVat = 0M;
                vatTable.AddCell(CreateCell(calculatedVat.ToString(_decimalMask)));
                vatTable.AddCell(CreateCell(this.Value.ToString(_decimalMask)));
            }
            else
            {
                vatTable.AddCell(CreateCell(value.ToString(_decimalMask)));
                vatTable.AddCell(CreateCell("23%"));
                decimal calculatedVat = value * 0.23M;
                this.Value = value + calculatedVat;
                vatTable.AddCell(CreateCell(calculatedVat.ToString(_decimalMask)));
                vatTable.AddCell(CreateCell(this.Value.ToString(_decimalMask)));
            }
            return vatTable;
        }

        private PdfPTable VatTableHeader()
        {
            PdfPTable table = new PdfPTable(4);
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            table.TotalWidth = (_rightMargin - _leftMargin) / 2;
            table.LockedWidth = true;
            float[] percentages = new float[4] { 0.3f, 0.2f, 0.2f, 0.3f };
            table.SetWidths(percentages);//, new Rectangle(leftMargin, y(500), rightMargin, y(400)));
            PdfPCell headerCell = CreateCell("VAT tax values [Wartości obliczone z cen bez podatku] [" + this.Currency + "]");
            headerCell.Colspan = 4;
            table.AddCell(headerCell);

            table.AddCell(CreateCell("Without tax [Bez podatku]"));
            table.AddCell(CreateCell("VAT rate [stawka VAT]"));
            table.AddCell(CreateCell("VAT"));
            table.AddCell(CreateCell("With tax [Z podatkiem]"));
            return table;
        }

        private PdfPTable MainTableAddSummary(PdfPTable table, decimal summary)
        {
            table.AddCell(CreateCell("", Element.ALIGN_LEFT, 0));
            table.AddCell(CreateCell("", Element.ALIGN_LEFT, 0));
            table.AddCell(CreateCell("", Element.ALIGN_LEFT, 0));
            table.AddCell(CreateCell("", Element.ALIGN_LEFT, 0));
            table.AddCell(CreateCell("Total [Razem]:", Element.ALIGN_RIGHT, 0));
            table.AddCell(CreateCell(summary.ToString(_decimalMask)));
            table.AddCell(CreateCell("", Element.ALIGN_LEFT, 0));
            return table;
        }

        private PdfPTable MainTableAddValues(PdfPTable table, decimal value, string serviceName)
        {
            if (Company == Company.Tyrecheck)
            {
                table.AddCell(CreateCell("1"));
                table.AddCell(CreateCell(serviceName, Element.ALIGN_LEFT));
                table.AddCell(CreateCell("1"));
                table.AddCell(CreateCell(GetPaymentType()));
                table.AddCell(CreateCell(value.ToString(_decimalMask)));
                table.AddCell(CreateCell(value.ToString(_decimalMask)));
                table.AddCell(CreateCell("np"));
            }
            else if (Company == Company.Tirecheck)
            {
                table.AddCell(CreateCell("1"));
                table.AddCell(CreateCell(serviceName, Element.ALIGN_LEFT));
                table.AddCell(CreateCell(NumberOfDays.ToString()));
                table.AddCell(CreateCell(GetPaymentType()));
                table.AddCell(CreateCell(PerDay.ToString(_decimalMask)));
                table.AddCell(CreateCell(value.ToString(_decimalMask)));
                table.AddCell(CreateCell("np"));
            }
            else if (Company == Company.PaulRyan)
            {
                table.AddCell(CreateCell("1"));
                table.AddCell(CreateCell(serviceName, Element.ALIGN_LEFT));
                table.AddCell(CreateCell(NumberOfHours.ToString()));
                table.AddCell(CreateCell(GetPaymentType()));
                table.AddCell(CreateCell(PerHour.ToString(_decimalMask)));
                table.AddCell(CreateCell(value.ToString(_decimalMask)));
                table.AddCell(CreateCell("n/a [np]"));
            }
            else if (Company == Company.Coltech)
            {
                table.AddCell(CreateCell("1"));
                table.AddCell(CreateCell(serviceName, Element.ALIGN_LEFT));
                table.AddCell(CreateCell(NumberOfDays.ToString()));
                table.AddCell(CreateCell(GetPaymentType()));
                table.AddCell(CreateCell(PerDay.ToString(_decimalMask)));
                table.AddCell(CreateCell(value.ToString(_decimalMask)));
                table.AddCell(CreateCell("n/a [np]"));
            }
            else if (Company == Company.IgGroup)
            {
                table.AddCell(CreateCell("1"));
                table.AddCell(CreateCell(serviceName, Element.ALIGN_LEFT));
                table.AddCell(CreateCell(NumberOfHours.ToString()));
                table.AddCell(CreateCell(GetPaymentType()));
                table.AddCell(CreateCell(PerHour.ToString(_decimalMask)));
                table.AddCell(CreateCell(value.ToString(_decimalMask)));
                table.AddCell(CreateCell("23%"));
            }
            else
            {
                table.AddCell(CreateCell("1"));
                table.AddCell(CreateCell(serviceName, Element.ALIGN_LEFT));
                table.AddCell(CreateCell(NumberOfHours.ToString()));
                table.AddCell(CreateCell(GetPaymentType()));
                table.AddCell(CreateCell(PerHour.ToString(_decimalMask)));
                table.AddCell(CreateCell(value.ToString(_decimalMask)));
                table.AddCell(CreateCell("23%"));
            }
            return table;
        }

        private string GetPaymentType()
        {
            if (this.PaymentType == PaymentType.Hourly)
                return "h";
            else if (this.PaymentType == PaymentType.Daily)
                return "d";
            else if (this.PaymentType == PaymentType.Single)
                return "szt";
            else
                return "1";
        }

        private PdfPTable MainTableHeader()
        {
            PdfPTable table = new PdfPTable(7);
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            table.TotalWidth = _rightMargin - _leftMargin;
            table.LockedWidth = true;
            float[] percentages = new float[7] { 0.05f, 0.39f, 0.09f, 0.09f, 0.15f, 0.15f, 0.08f };
            table.SetWidths(percentages);//, new Rectangle(leftMargin, y(500), rightMargin, y(400)));
            table.AddCell(CreateCell("No. [Lp.]"));
            table.AddCell(CreateCell("Service Name [Nazwa towaru/uslugi]"));
            table.AddCell(CreateCell("Quantity [Ilosc]"));
            table.AddCell(CreateCell("Type [J.m.]"));
            table.AddCell(CreateCell("Unit price [Cena jednostkowa bez podatku] [" + this.Currency + "]"));
            table.AddCell(CreateCell("Without tax [Wartość bez podatku] [" + this.Currency + "]"));
            table.AddCell(CreateCell("VAT rate [Stawka VAT]"));
            return table;
        }

        private PdfPCell CreateCell(string text, int alignment = Element.ALIGN_CENTER, int border = 15)
        {
            Font font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 10f, Font.NORMAL, new BaseColor(0, 0, 0));
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = alignment;
            cell.VerticalAlignment = alignment;
            cell.PaddingBottom = 6;
            cell.Border = border;
            return cell;
        }

        /// <summary>
        /// Filled bar with centered text
        /// </summary>
        /// <param name="llx">Low left x</param>
        /// <param name="lly">Low left y</param>
        /// <param name="urx">Upper right x</param>
        /// <param name="ury">Upper right y</param>
        private void Bar(float llx, float lly, float urx, float ury, float height, string text, int alignment = Element.ALIGN_CENTER)
        {
            _canvas.SetLineWidth(0.5f);
            _canvas.SetColorFill(new BaseColor(211, 211, 211));
            _canvas.RoundRectangle(llx, lly, urx - llx, height, 3);
            _canvas.ClosePathFillStroke();
            Rectangle rect = new Rectangle(llx, lly, urx, ury);
            ColumnText ct = new ColumnText(_canvas);
            ct.SetSimpleColumn(rect.Left, rect.Bottom, rect.Right, rect.Top);
            ct.Alignment = alignment;
            _canvas.SetColorFill(new BaseColor(0, 0, 0));
            Font font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, 13f, Font.BOLD, new BaseColor(0, 0, 0));
            Paragraph p = new Paragraph(text, font);
            p.Alignment = alignment;

            ct.AddElement(p);
            ct.Go();
        }

        // GenerateInvoiceNo moved to PdfHelpers.GenerateInvoiceNo to allow unit testing and reuse.

        private void Divider(float y)
        {
            _canvas.SetLineWidth(0.5f);
            _canvas.MoveTo(_leftMargin, y);
            _canvas.LineTo(_rightMargin, y);
            _canvas.ClosePath();
            _canvas.Stroke();
        }

        private void Signature(float x, float y, string text)
        {
            _canvas.SetLineWidth(0.5f);
            _canvas.MoveTo(x, y);
            _canvas.LineTo(x + _w / 3, y);
            SetText(x + 10, y - 6, 6f, text);
            _canvas.ClosePath();
            _canvas.Stroke();
        }

        private void SetText(float x, float y, float size, string text)
        {
            Font font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, size, Font.NORMAL, new BaseColor(0, 0, 0));
            Phrase phrase = new Phrase(text, font);
            ColumnText.ShowTextAligned(_canvas, Element.ALIGN_LEFT, phrase, x, y, 0);
        }

        private void SetTextBold(float x, float y, float size, string text)
        {
            Font font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, true, size, Font.BOLD, new BaseColor(0, 0, 0));
            Phrase phrase = new Phrase(text, font);
            ColumnText.ShowTextAligned(_canvas, Element.ALIGN_LEFT, phrase, x, y, 0);
        }

        private float x(float f)
        {
            return f;
        }

        private float y(float f)
        {
            return PageSize.A4.Height - f;
        }

        private void Seller()
        {
            SetText(_leftMargin, y(30), 8f, "Seller [Sprzedawca]:");
            Divider(y(32));
            int dif = 12;
            int yy = 45 - dif;
            float textSize = 9;
            SetTextBold(_leftMargin, y(yy += dif), textSize, @"Name [Nazwa]:");
            SetText(_leftMargin, y(yy += dif), textSize, @"Address [Adres]:");
            SetTextBold(_leftMargin, y(yy += dif), textSize, @"Tax ID [NIP]:");
            SetText(_leftMargin, y(yy += dif), textSize, @"Registration no [Regon]:");
            SetText(_leftMargin, y(yy += dif), textSize, @"Phone [Telefon]:");
            SetText(_leftMargin, y(yy += dif), textSize, @"Email:");
            SetText(_leftMargin, y(yy += dif), textSize, @"Webpage [Strona]:");
            SetTextBold(_leftMargin, y(yy += dif), textSize, @"Account [Rachunek]:");
            SetText(_leftMargin, y(yy += dif), textSize, @"SWIFT, BIC:");
            SetText(_leftMargin, y(yy += dif), textSize, @"Bank name [Nazwa banku]:");
            SetText(_leftMargin, y(yy += dif), textSize, @"Bank address [Adres banku]:");

            yy = 45 - dif;
            SetTextBold(_leftMargin160, y(yy += dif), textSize, @"Your Company Name, Owner Name");
            SetText(_leftMargin160, y(yy += dif), textSize, @"00-000 City, Street 1/1, Country");
            SetTextBold(_leftMargin160, y(yy += dif), textSize, @"PL 0000000000");
            SetText(_leftMargin160, y(yy += dif), textSize, @"000000000");
            SetText(_leftMargin160, y(yy += dif), textSize, @"(+48) 000-000-000");
            SetText(_leftMargin160, y(yy += dif), textSize, @"email@example.com");
            SetText(_leftMargin160, y(yy += dif), textSize, @"https://example.com");
            if (Company == Company.PaulRyan)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"PL00 0000 0000 0000 0000 0000 0000"); //GBP
            }
            else if (Company == Company.Coltech)
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"PL00 0000 0000 0000 0000 0000 0000"); //EUR
            }
            else
            {
                SetTextBold(_leftMargin160, y(yy += dif), textSize, @"00 0000 0000 0000 0000 0000 0000"); //PLN
            }
            SetText(_leftMargin160, y(yy += dif), textSize, @"XXXXXXXXXXX");
            SetText(_leftMargin160, y(yy += dif), textSize, @"Bank Name");
            SetText(_leftMargin160, y(yy += dif), textSize, @"Bank Street 1, 00-000 City");
        }

        private void Setup()
        {
            _doc = new Document();
            _doc.SetPageSize(PageSize.A4);
            _size = _doc.PageSize;
            _w = _size.Width;
            //595x842

            _doc.AddTitle("Invoice");
            _decimalMask = "0.00";

            _leftMargin = x(20);
            _leftMargin160 = x(160);
            _leftMargin190 = x(190);
            _rightMargin = x(_w - 20);
            // Create FileStream explicitly so we can ensure it's closed before opening the file externally.
            // If the target file is locked by another process (OneDrive, viewer), try alternate unique filenames.
            string chosenPath = _file;
            int attempt = 0;
            while (true)
            {
                try
                {
                    _fileStream = new System.IO.FileStream(chosenPath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
                    _writer = PdfWriter.GetInstance(_doc, _fileStream);
                    // persist the actual file path used
                    _file = chosenPath;
                    break;
                }
                catch (System.IO.IOException)
                {
                    attempt++;
                    // generate an alternative filename with timestamp to avoid collisions/locks
                    string dir = System.IO.Path.GetDirectoryName(_file) ?? System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                    string name = System.IO.Path.GetFileNameWithoutExtension(_file);
                    string ext = System.IO.Path.GetExtension(_file);
                    string alt = string.Format("{0}-{1}-{2}{3}", name, DateTime.Now.ToString("yyyyMMddHHmmss"), attempt, ext);
                    chosenPath = System.IO.Path.Combine(dir, alt);
                    if (attempt >= 10)
                    {
                        // give up after several attempts
                        throw;
                    }
                    System.Threading.Thread.Sleep(150);
                }
            }

            _doc.Open();
            _writer.CompressionLevel = 0;
            _canvas = _writer.DirectContentUnder;
            _canvas.SetLineWidth(0.5f);
            _canvas.SetColorStroke(new BaseColor(169, 169, 169));
        }
    }
}
