using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Diagnostics;
using PdfDavITFutures.Service;

namespace PdfDavITFutures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rbIgGroup.IsChecked = true;
            rbPLN.IsChecked = true;
            rbHourly.IsChecked = true;
            rbCompany_Click(this, null);
            var now = DateTime.Now;
            DateOfIssue.SelectedDate = now;
            var nextMonth = now.AddMonths(1);
            Deadline.SelectedDate = new DateTime(nextMonth.Year, nextMonth.Month, 17);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PdfService pdf = new PdfService(DateOfIssue.SelectedDate.Value, Deadline.SelectedDate.Value, ValueOfInvoice.Text, NumberOfDays.Text, NumberOfHours.Text, SelectedCompany(), SelectedCurrency(), SelectedPaymentType(), InvoiceNo.Text, ServiceName.Text);
            try
            {
                Debug.WriteLine("[MainWindow] Starting PDF generation");
                pdf.GeneratePdf();
                Debug.WriteLine($"[MainWindow] PDF generated: {pdf.GeneratedFilePath}");
                MessageBox.Show($"PDF wygenerowany:\n{pdf.GeneratedFilePath}", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[MainWindow] PDF generation failed: " + ex);
                MessageBox.Show($"Błąd podczas generowania PDF:\n{ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Company? SelectedCompany()
        {
            if (rbTyrecheck.IsChecked.Value)
                return Company.Tyrecheck;
            else if (rbTirecheck.IsChecked.Value)
                return Company.Tirecheck;
            else if (rbITKontrakt.IsChecked.Value)
                return Company.ITKontrakt;
            else if (rbIntive.IsChecked.Value)
                return Company.Intive;
            else if (rbCododile.IsChecked.Value)
                return Company.Cododile;
            else if (rbPaulRyan.IsChecked.Value)
                return Company.PaulRyan;
            else if (rbSII.IsChecked.Value)
                return Company.SII;
            else if (rbColtech.IsChecked.Value)
                return Company.Coltech;
            else if (rbIgGroup.IsChecked.Value)
                return Company.IgGroup;
            else if (rbIgGroupBenefit.IsChecked.Value)
                return Company.IgGroupBenefit;
            else
                return null;
        }

        private Currency SelectedCurrency()
        {
            if (rbEUR.IsChecked.Value)
                return Currency.EUR;
            else if (rbGBP.IsChecked.Value)
                return Currency.GBP;
            else
                return Currency.PLN;
        }

        private PaymentType SelectedPaymentType()
        {
            if (rbDaily.IsChecked.Value)
                return PaymentType.Daily;
            else if (rbHourly.IsChecked.Value)
                return PaymentType.Hourly;
            else
                return PaymentType.Single;
        }

        private void rbCompany_Click(object sender, RoutedEventArgs e)
        {
            string standardServiceName = "Uslugi informatyczne (IT services)";
            if (rbTyrecheck.IsChecked.Value)
            {
                lblValue.Content = "Kwota:";
                SetValueHint("1480");
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Collapsed;
                NumberOfHours.Visibility = Visibility.Collapsed;
                rbEUR.IsChecked = true;
                ServiceName.Text = "Additional costs when providing IT services(dodatkowe koszty przy swiadczeniu uslug informatycznych)";
            }
            else if (rbTirecheck.IsChecked.Value)
            {
                lblValue.Content = "Stawka dzienna:";
                SetValueHint("140");
                NumberOfDays.Text = "21";
                lblNumberOfDays.Visibility = Visibility.Visible;
                NumberOfDays.Visibility = Visibility.Visible;
                lblNumberOfHours.Visibility = Visibility.Collapsed;
                NumberOfHours.Visibility = Visibility.Collapsed;
                rbEUR.IsChecked = true;
                ServiceName.Text = "Uslugi informatyczne(IT services)";
            }
            else if (rbITKontrakt.IsChecked.Value)
            {
                lblValue.Content = "Stawka godzinowa:";
                SetValueHint("115");
                NumberOfHours.Text = "168";
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Visible;
                NumberOfHours.Visibility = Visibility.Visible;
                rbPLN.IsChecked = true;
                ServiceName.Text = standardServiceName;
            }
            else if (rbIntive.IsChecked.Value)
            {
                lblValue.Content = "Stawka godzinowa:";
                SetValueHint("145");
                NumberOfHours.Text = "168";
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Visible;
                NumberOfHours.Visibility = Visibility.Visible;
                rbPLN.IsChecked = true;
                ServiceName.Text = standardServiceName;
            }
            else if (rbCododile.IsChecked.Value)
            {
                lblValue.Content = "Stawka godzinowa:";
                SetValueHint("108");
                NumberOfHours.Text = "168";
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Visible;
                NumberOfHours.Visibility = Visibility.Visible;
                rbPLN.IsChecked = true;
                ServiceName.Text = standardServiceName;
            }
            else if (rbPaulRyan.IsChecked.Value)
            {
                lblValue.Content = "Stawka godzinowa:";
                SetValueHint("50");
                NumberOfHours.Text = "8";
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Visible;
                NumberOfHours.Visibility = Visibility.Visible;
                rbGBP.IsChecked = true;
                ServiceName.Text = "IT services [Uslugi informatyczne]";
            }
            else if (rbSII.IsChecked.Value)
            {
                lblValue.Content = "Stawka godzinowa:";
                SetValueHint("185");
                NumberOfHours.Text = "88";
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Visible;
                NumberOfHours.Visibility = Visibility.Visible;
                rbPLN.IsChecked = true;
                ServiceName.Text = standardServiceName;
            }
            else if (rbColtech.IsChecked.Value)
            {
                lblValue.Content = "Stawka dzienna:";
                SetValueHint("400");
                NumberOfDays.Text = "21";
                lblNumberOfDays.Visibility = Visibility.Visible;
                NumberOfDays.Visibility = Visibility.Visible;
                lblNumberOfHours.Visibility = Visibility.Collapsed;
                NumberOfHours.Visibility = Visibility.Collapsed;
                rbEUR.IsChecked = true;
                ServiceName.Text = "IT services [Uslugi informatyczne]";
            }
            else if (rbIgGroup.IsChecked.Value)
            {
                lblValue.Content = "Stawka godzinowa:";
                SetValueHint("230");
                rbHourly.IsChecked = true;
                NumberOfHours.Text = "160";
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Visible;
                NumberOfHours.Visibility = Visibility.Visible;
                rbPLN.IsChecked = true;
                ServiceName.Text = "IT services [Uslugi informatyczne]. Purchase Order (PO): 13966";
            }
            else if (rbIgGroupBenefit.IsChecked.Value)
            {
                lblValue.Content = "Kwota:";
                SetValueHint("850");
                rbSingle.IsChecked = true;
                NumberOfHours.Text = "1";
                lblNumberOfDays.Visibility = Visibility.Collapsed;
                NumberOfDays.Visibility = Visibility.Collapsed;
                lblNumberOfHours.Visibility = Visibility.Visible;
                NumberOfHours.Visibility = Visibility.Visible;
                rbPLN.IsChecked = true;
                ServiceName.Text = "Remuneration under point 4 of paragraph 4 of the contract. Purchase Order (PO): 13966";
            }
        }

        /// <summary>
        /// Clears the amount field and shows an example value as a tooltip instead of a pre-filled rate.
        /// </summary>
        private void SetValueHint(string example)
        {
            ValueOfInvoice.Text = "";
            ValueOfInvoice.ToolTip = "np. " + example;
        }

        private void rbUnit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
