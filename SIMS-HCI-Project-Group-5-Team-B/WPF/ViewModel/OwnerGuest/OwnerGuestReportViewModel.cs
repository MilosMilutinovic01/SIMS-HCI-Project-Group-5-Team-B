using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class OwnerGuestReportViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private ReservationService reservationService;
        private OwnerGuestService ownerGuestService;
        private int ownerGuestId;
        private Nullable<DateTime> start;
        public Nullable<DateTime> Start
        {
            get { return start; }
            set
            {
                if (value != start)
                {
                    start = value;
                    NotifyPropertyChanged(nameof(Start));
                }
            }
        }
        private Nullable<DateTime> end;
        public Nullable<DateTime> End
        {
            get { return end; }
            set
            {
                if (value != end)
                {
                    end = value;
                    NotifyPropertyChanged(nameof(End));
                }
            }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    NotifyPropertyChanged(nameof(Type));
                }
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Start")
                {
                    if (Start == null)
                    {
                        return "You must select Start";
                    }

                }

                if (columnName == "End")
                {
                    if (End == null)
                    {
                        return "You must select End";
                    }
                    else if (End < Start)
                    {
                        return "End must be after Start";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Start", "End" };
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



        public event PropertyChangedEventHandler? PropertyChanged;
        private OwnerGuest ownerGUest;
        //Commands
        public RelayCommand CLoseCommand { get; }
        public RelayCommand GenerateCommand { get; }

        public OwnerGuestReportViewModel(int ownerGuestId)
        {
            this.ownerGuestId = ownerGuestId;
            reservationService = new ReservationService();
            ownerGuestService = new OwnerGuestService();

            Type = "Scheduled Reservations";
            ownerGUest = ownerGuestService.GetById(this.ownerGuestId);

            CLoseCommand = new RelayCommand(OnClose);
            GenerateCommand = new RelayCommand(OnGenerate);

        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void OnClose()
        {
            App.Current.Windows.OfType<ReportWindow>().FirstOrDefault().Close();
        }

        public void OnGenerate()
        {
            if (this.IsValid)
            {
                List<Reservation> reservations= new List<Reservation>();

                if(Type == "Scheduled Reservations")
                {
                    reservations = reservationService.GetReservationsInTimeSpan((DateTime)Start, (DateTime)End, ownerGuestId);
                }
                else
                {
                    reservations = reservationService.GetCanceledInTimeSpan((DateTime)Start, (DateTime)End, ownerGuestId);
                }
                byte[] pdfBytes = GeneratePDFDocument(reservations);
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), ownerGUest.Username + " Reservations " + ((DateTime)Start).ToLongDateString() + "-" + ((DateTime)End).ToLongDateString() + ".pdf");
                SavePDFDocument(pdfBytes, filePath);

                // Prikazivanje PDF dokumenta u novom prozoru
                PdfWindow pdfWindow = new PdfWindow(filePath);
                
                pdfWindow.Show();
                App.Current.Windows.OfType<ReportWindow>().FirstOrDefault().Close();
            }
            else
            {
                MessageBox.Show("Some fields are not filled!", "Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SavePDFDocument(byte[] pdfBytes, string filePath)
        {
            File.WriteAllBytes(filePath, pdfBytes);
        }

        private byte[] GeneratePDFDocument(List<Reservation> reservations)
        {

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (MemoryStream ms = new MemoryStream())
            {

                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage(); 
                XGraphics gfx = XGraphics.FromPdfPage(page);

                
                var fontHeading = new XFont("Arial", 18, XFontStyle.BoldItalic);
                var font = new XFont("Arial", 12, XFontStyle.Regular);
                XBrush brush = XBrushes.Black;
               
                double x = 50;
                double y = 50;
                double space = 20;

                gfx.DrawString("Guest: " + ownerGUest.Name + " " + ownerGUest.Surname, font, brush, x, y);
                y += space;
                gfx.DrawString("Generated: " + DateTime.Today.ToShortDateString(), font, brush, x, y);
                y += space;
                gfx.DrawString("Start: " + ((DateTime)Start).ToShortDateString(), font, brush, x, y);
                y += space;
                gfx.DrawString("End: " + ((DateTime)End).ToShortDateString(), font, brush, x, y);
                y += space;
                gfx.DrawString("Reservations type: " + Type, font, brush, x, y);

                y += space;
                y += space;
                y += space;
                gfx.DrawString(Type + " From: " + ((DateTime)Start).ToShortDateString() + " To:" + ((DateTime)End).ToShortDateString(), fontHeading, brush, x, y);





                // Dodajte naslov


                // Povećajte Y koordinatu za razmak između naslova i stavki
                y += space;
                y += space;
                // Dodajte rezervacije
                if(reservations.Count() == 0)
                {
                    gfx.DrawString($"There were no {Type} in this period", font, brush, x, y);
                }
                foreach (Reservation reservation in reservations)
                { 
                    gfx.DrawString($"Accommodation: {reservation.Accommodation.Name}, Start: {reservation.StartDate.ToShortDateString()} , End: {reservation.EndDate.ToShortDateString()}, Owner:{reservation.Accommodation.Owner.Name} {reservation.Accommodation.Owner.Surname}", font, brush, x, y);
                    y += space;

                }

                document.Save(ms, false);

                return ms.ToArray();
            }
        }




    }
}
