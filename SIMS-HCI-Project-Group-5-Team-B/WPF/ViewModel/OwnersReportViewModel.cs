using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class OwnersReportViewModel: IDataErrorInfo, INotifyPropertyChanged
    {

        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand GeneratePreviewCommand { get; }
        public Accommodation SelectedAccommodation { get; set; }
        private ReservationService reservationService;

        public OwnersReportViewModel(Accommodation SelectedAccommodation, ReservationService reservationService)
        {
            SelectedStartDate = DateTime.Today;
            SelectedEndDate = DateTime.Today;
            this.SelectedAccommodation = SelectedAccommodation;
            this.reservationService = reservationService;
            CloseCommand = new RelayCommand(CloseExecute, CloseCanExecute);
            GeneratePreviewCommand = new RelayCommand(GeneratePreviewExecute, GeneratePreviewCanExecute);
        }

        public bool GeneratePreviewCanExecute()
        {
            return true;
        }
        public void GeneratePreviewExecute()
        {
            List<Reservation> reservations = reservationService.GetAccommodationReservationsInTimeSpan(SelectedAccommodation,SelectedStartDate,SelectedEndDate);

            if (reservations.Count == 0)
            {

                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    string sMessageBoxText = $"Are you sure you want generate report because there are no reservations?";
                    string sCaption = "Confirm";
                    MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                    MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                    if (result == MessageBoxResult.Yes)
                    {
                        byte[] pdfBytes = GeneratePDFDocument(reservations);
                        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SelectedAccommodation.Name + " Reservations " + SelectedStartDate.ToLongDateString() + "-" + SelectedEndDate.ToLongDateString() + ".pdf");
                        SavePDFDocument(pdfBytes, filePath);

                        // Prikazivanje PDF dokumenta u novom prozoru
                        PdfWindow pdfWindow = new PdfWindow(filePath);

                        pdfWindow.Show();
                        App.Current.Windows[4].Close();
                    }
                }
                else
                {
                    string sMessageBoxText = $"Da li ste sigurni da zelite da generisete izvestaj jer nema rezervacija?";
                    string sCaption = "Potrvrda";
                    MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                    MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                    if (result == MessageBoxResult.Yes)
                    {
                        byte[] pdfBytes = GeneratePDFDocument(reservations);
                        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SelectedAccommodation.Name + " Rezervacije " + SelectedStartDate.ToLongDateString() + "-" + SelectedEndDate.ToLongDateString() + ".pdf");
                        SavePDFDocument(pdfBytes, filePath);

                        // Prikazivanje PDF dokumenta u novom prozoru
                        PdfWindow pdfWindow = new PdfWindow(filePath);

                        pdfWindow.Show();
                        App.Current.Windows[4].Close();
                    }
                }
              

                
               
            }
            else {
                byte[] pdfBytes = GeneratePDFDocument(reservations);
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), SelectedAccommodation.Name + " Reservations " + SelectedStartDate.ToLongDateString() + "-" + SelectedEndDate.ToLongDateString() + ".pdf");
                SavePDFDocument(pdfBytes, filePath);

                // Prikazivanje PDF dokumenta u novom prozoru
                PdfWindow pdfWindow = new PdfWindow(filePath);

                pdfWindow.Show();
                App.Current.Windows[4].Close();
            }
        }

        private void SavePDFDocument(byte[] pdfBytes, string filePath)
        {
            File.WriteAllBytes(filePath, pdfBytes);
        }

        public bool CloseCanExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            App.Current.Windows[4].Close();
        }


        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                
                if (columnName == "SelectedEndDate")
                {
                    if (SelectedStartDate > SelectedEndDate)
                    {
                        if (Properties.Settings.Default.currentLanguage == "en-US")
                        {
                            return "Start date can not\n" + "be bigger than end date";
                        }
                        else
                        {
                            return "Datum pocetka ne moze\n" + "biti veci od datuma kraja";
                        }
                    }
                }

                return null;
            }
        }


        private readonly string[] _validatedProperties = { "SelectedStartDate", "SelectedEndDate"};

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }


        private byte[] GeneratePDFDocument(List<Reservation> reservations)
        {

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (MemoryStream ms = new MemoryStream())
            {
                // Kreirajte novi PDF dokument
                PdfDocument document = new PdfDocument();

                // Dodajte novu stranicu
                PdfPage page = document.AddPage();

                // Kreirajte grafički objekat za crtanje na stranici
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Definišite font i boju teksta
                var fontHeading = new XFont("Arial",18, XFontStyle.Underline);
                var font = new XFont("Arial", 12, XFontStyle.Regular);
                XBrush brush = XBrushes.Black;

                // Definišite poziciju i razmak između stavki
                double x = 50;
                double y = 50;
                double razmak = 20;



                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    gfx.DrawString("Owner: " + SelectedAccommodation.Owner.Name + " " + SelectedAccommodation.Owner.Surname, font, brush, x, y);
                    y += razmak;
                    gfx.DrawString("Accommodation: " + SelectedAccommodation.Name, font, brush, x, y);
                    y += razmak;
                    gfx.DrawString("Location: " + SelectedAccommodation.Location.City + "," + SelectedAccommodation.Location.State, font, brush, x, y);
                    y += razmak;
                    gfx.DrawString("Generated: " + DateTime.Today.ToLongDateString(), font, brush, x, y);
                    y += razmak;
                    y += razmak;
                    y += razmak;
                    gfx.DrawString("Reservations in time span: " + SelectedStartDate.ToShortDateString() + " - " + SelectedEndDate.ToShortDateString(), fontHeading, brush, x, y);
                }
                else
                {
                    gfx.DrawString("Vlasnik: " + SelectedAccommodation.Owner.Name + " " + SelectedAccommodation.Owner.Surname, font, brush, x, y);
                    y += razmak;
                    gfx.DrawString("Smestaj: " + SelectedAccommodation.Name, font, brush, x, y);
                    y += razmak;
                    gfx.DrawString("Lokacija: " + SelectedAccommodation.Location.City + "," + SelectedAccommodation.Location.State, font, brush, x, y);
                    y += razmak;
                    gfx.DrawString("Generisano: " + DateTime.Today.ToLongDateString(), font, brush, x, y);
                    y += razmak;
                    y += razmak;
                    y += razmak;
                    gfx.DrawString("Rezervacije u vremenskom periodu: " + SelectedStartDate.ToShortDateString() + " - " + SelectedEndDate.ToShortDateString(), fontHeading, brush, x, y);
                }




                // Dodajte naslov
                

                // Povećajte Y koordinatu za razmak između naslova i stavki
                y += razmak;
                y += razmak;
                // Dodajte rezervacije
                foreach (Reservation reservation in reservations)
                {

                    if (Properties.Settings.Default.currentLanguage == "en-US")
                    {
                        gfx.DrawString($"Accommodation: {reservation.Accommodation.Name}, Start: { reservation.StartDate.ToShortDateString()} , End: {reservation.EndDate.ToShortDateString()}, Guest:{reservation.OwnerGuest.Name} {reservation.OwnerGuest.Surname}", font, brush, x, y);
                    }
                    else
                    {
                        gfx.DrawString($"Smestaj: {reservation.Accommodation.Name}, Pocetak: { reservation.StartDate.ToShortDateString()} , Kraj: {reservation.EndDate.ToShortDateString()}, Gost:{reservation.OwnerGuest.Name} {reservation.OwnerGuest.Surname}", font, brush, x, y);
                    }

                    // Povećajte Y koordinatu za razmak između stavki
                    y += razmak;

                }

                // Sačuvajte PDF dokument u memoriji
                document.Save(ms, false);

                // Vratite bajtovski niz sa sadržajem PDF dokumenta
                return ms.ToArray();
            }
        }





    }
}
