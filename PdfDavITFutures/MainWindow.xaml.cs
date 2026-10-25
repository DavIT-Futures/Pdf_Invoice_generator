using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using PdfDavITFutures.Service;

namespace PdfDavITFutures
{
    public class CompanyOption
    {
        public Company Company { get; set; }
        public string DisplayName { get; set; } = string.Empty;

        public override string ToString() => DisplayName;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitCompanies();
            var now = DateTime.Now;
            DateOfIssue.SelectedDate = now;
            var nextMonth = now.AddMonths(1);
            Deadline.SelectedDate = new DateTime(nextMonth.Year, nextMonth.Month, 17);
        }

        private void InitCompanies()
        {
            cbCompany.ItemsSource = new List<CompanyOption>
            {
                new() { Company = Company.IgGroup, DisplayName = "IG Group (L2 Dealer, FinTech - UK)" },
                new() { Company = Company.IgGroupBenefit, DisplayName = "IG Group Benefit (UK)" },
                new() { Company = Company.CreditSuisse, DisplayName = "Credit Suisse (LTV / Banking - PL/CH)" },
                new() { Company = Company.Qiagen, DisplayName = "Qiagen (Life Sciences / MedTech - PL/DE)" },
                new() { Company = Company.Tyrecheck, DisplayName = "Tyrecheck (Automotive IoT - IE)" },
                new() { Company = Company.Tirecheck, DisplayName = "Tirecheck s.r.o. (Automotive IoT - CZ)" },
                new() { Company = Company.Vitalograph, DisplayName = "Vitalograph (MedTech - IE)" },
                new() { Company = Company.AmcsGroup, DisplayName = "AMCS Group (Logistics IoT - IE)" },
                new() { Company = Company.ExorigoProService, DisplayName = "Exorigo / ProService AT (Banking - PL)" },
                new() { Company = Company.Focis, DisplayName = "Focis.co.uk (LegalTech SaaS - UK)" },
                new() { Company = Company.ITKontrakt, DisplayName = "IT Kontrakt (Avenga - PL)" },
                new() { Company = Company.Intive, DisplayName = "intive GmbH (DE/PL)" },
                new() { Company = Company.SII, DisplayName = "SII Poland (PL)" },
                new() { Company = Company.Cododile, DisplayName = "Cododile (PL)" },
                new() { Company = Company.Coltech, DisplayName = "Coltech Recruitment (UK)" },
                new() { Company = Company.PaulRyan, DisplayName = "Paul Ryan (UK)" },
            };
            cbCompany.SelectedIndex = 0;
        }

        private void cbCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCompany.SelectedItem is CompanyOption opt)
            {
                ApplyCompanyDefaults(opt.Company);
            }
        }

        private void ApplyCompanyDefaults(Company company)
        {
            string standardServiceName = "Uslugi informatyczne (IT services)";

            switch (company)
            {
                case Company.IgGroup:
                    rbPLN.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("230");
                    NumberOfHours.Text = "160";
                    ServiceName.Text = "IT services [Uslugi informatyczne]. Purchase Order (PO): 13966";
                    break;

                case Company.IgGroupBenefit:
                    rbPLN.IsChecked = true;
                    rbSingle.IsChecked = true;
                    SetValueHint("850");
                    NumberOfHours.Text = "1";
                    ServiceName.Text = "Remuneration under point 4 of paragraph 4 of the contract. Purchase Order (PO): 13966";
                    break;

                case Company.CreditSuisse:
                    rbPLN.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("160");
                    NumberOfHours.Text = "168";
                    ServiceName.Text = "Uslugi informatyczne (IT services) - LTV / Risk Platform";
                    break;

                case Company.Qiagen:
                    rbPLN.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("150");
                    NumberOfHours.Text = "168";
                    ServiceName.Text = "Uslugi informatyczne (IT services) - Diagnostics & Cloud API";
                    break;

                case Company.Tyrecheck:
                    rbEUR.IsChecked = true;
                    rbSingle.IsChecked = true;
                    SetValueHint("1480");
                    ServiceName.Text = "Additional costs when providing IT services(dodatkowe koszty przy swiadczeniu uslug informatycznych)";
                    break;

                case Company.Tirecheck:
                    rbEUR.IsChecked = true;
                    rbDaily.IsChecked = true;
                    SetValueHint("140");
                    NumberOfDays.Text = "21";
                    ServiceName.Text = "Uslugi informatyczne(IT services)";
                    break;

                case Company.Vitalograph:
                    rbEUR.IsChecked = true;
                    rbDaily.IsChecked = true;
                    SetValueHint("350");
                    NumberOfDays.Text = "20";
                    ServiceName.Text = "IT services [Uslugi informatyczne] - Respiratory Diagnostics";
                    break;

                case Company.AmcsGroup:
                    rbEUR.IsChecked = true;
                    rbDaily.IsChecked = true;
                    SetValueHint("320");
                    NumberOfDays.Text = "21";
                    ServiceName.Text = "IT services [Uslugi informatyczne] - WasteTech & Telematics";
                    break;

                case Company.ExorigoProService:
                    rbPLN.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("120");
                    NumberOfHours.Text = "168";
                    ServiceName.Text = "Uslugi informatyczne (IT services) - Fund Distribution System";
                    break;

                case Company.Focis:
                    rbGBP.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("55");
                    NumberOfHours.Text = "80";
                    ServiceName.Text = "IT services [Uslugi informatyczne] - Case Management SaaS";
                    break;

                case Company.ITKontrakt:
                    rbPLN.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("115");
                    NumberOfHours.Text = "168";
                    ServiceName.Text = standardServiceName;
                    break;

                case Company.Intive:
                    rbPLN.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("145");
                    NumberOfHours.Text = "168";
                    ServiceName.Text = standardServiceName;
                    break;

                case Company.SII:
                    rbPLN.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("185");
                    NumberOfHours.Text = "88";
                    ServiceName.Text = standardServiceName;
                    break;

                case Company.Cododile:
                    rbPLN.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("108");
                    NumberOfHours.Text = "168";
                    ServiceName.Text = standardServiceName;
                    break;

                case Company.Coltech:
                    rbEUR.IsChecked = true;
                    rbDaily.IsChecked = true;
                    SetValueHint("400");
                    NumberOfDays.Text = "21";
                    ServiceName.Text = "IT services [Uslugi informatyczne]";
                    break;

                case Company.PaulRyan:
                    rbGBP.IsChecked = true;
                    rbHourly.IsChecked = true;
                    SetValueHint("50");
                    NumberOfHours.Text = "8";
                    ServiceName.Text = "IT services [Uslugi informatyczne]";
                    break;
            }

            UpdateUnitVisibility();
        }

        private void rbUnit_Click(object sender, RoutedEventArgs e)
        {
            UpdateUnitVisibility();
        }

        private void UpdateUnitVisibility()
        {
            if (rbDaily.IsChecked == true)
            {
                lblValue.Content = "Stawka dzienna:";
                lblNumberOfDays.Visibility = Visibility.Visible;
                NumberOfDays.Visibility = Visibility.Visible;
                lblNumberOfHours.Visibility = Visibility.Collapsed;
                NumberOfHours.Visibility = Visibility.Collapsed;
            }
            else if (rbHourly.IsChecked == true)
            {
                lblValue.Content = "Stawka godzinowa:";
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Visible;
                NumberOfHours.Visibility = Visibility.Visible;
            }
            else
            {
                lblValue.Content = "Kwota:";
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Collapsed;
                NumberOfHours.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PdfService pdf = new PdfService(
                DateOfIssue.SelectedDate ?? DateTime.Now,
                Deadline.SelectedDate ?? DateTime.Now.AddDays(14),
                ValueOfInvoice.Text,
                NumberOfDays.Text,
                NumberOfHours.Text,
                SelectedCompany(),
                SelectedCurrency(),
                SelectedPaymentType(),
                InvoiceNo.Text,
                ServiceName.Text);

            try
            {
                Debug.WriteLine("[MainWindow] Starting PDF generation");
                pdf.GeneratePdf();
                Debug.WriteLine($"[MainWindow] PDF generated: {pdf.GeneratedFilePath}");
                var result = MessageBox.Show($"PDF wygenerowany:\n{pdf.GeneratedFilePath}\n\nCzy chcesz otworzyć folder z plikiem?", "Sukces", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    var folder = Path.GetDirectoryName(pdf.GeneratedFilePath);
                    if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = folder,
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[MainWindow] PDF generation failed: " + ex);
                MessageBox.Show($"Błąd podczas generowania PDF:\n{ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Company? SelectedCompany()
        {
            return (cbCompany.SelectedItem as CompanyOption)?.Company ?? Company.IgGroup;
        }

        private Currency SelectedCurrency()
        {
            if (rbEUR.IsChecked == true)
                return Currency.EUR;
            else if (rbGBP.IsChecked == true)
                return Currency.GBP;
            else
                return Currency.PLN;
        }

        private PaymentType SelectedPaymentType()
        {
            if (rbDaily.IsChecked == true)
                return PaymentType.Daily;
            else if (rbHourly.IsChecked == true)
                return PaymentType.Hourly;
            else
                return PaymentType.Single;
        }

        /// <summary>
        /// Clears the amount field and shows an example value as a tooltip instead of a pre-filled rate.
        /// </summary>
        private void SetValueHint(string example)
        {
            ValueOfInvoice.Text = "";
            ValueOfInvoice.ToolTip = "np. " + example;
        }
    }
}