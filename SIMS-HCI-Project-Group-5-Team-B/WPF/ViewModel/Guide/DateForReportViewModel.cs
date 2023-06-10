using iTextSharp.text.pdf;
using iTextSharp.text;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using System.Windows.Markup;
using Image = iTextSharp.text.Image;
using Font = iTextSharp.text.Font;
using Paragraph = iTextSharp.text.Paragraph;
using System.Diagnostics;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.Guide
{
    public class DateForReportViewModel : ViewModel
    {
        #region fields
        private DateTime selectedFirst;
        public DateTime SelectedFirst
        {
            get { return selectedFirst; }
            set
            {
                if (selectedFirst != value)
                {
                    selectedFirst = value;
                    if (SelectedSecond < SelectedFirst)
                        ErrorMessage = "Must select end date greater than start date!";
                    else
                        ErrorMessage = "";
                    OnPropertyChanged(nameof(SelectedFirst));
                }
            }
        }
        private DateTime selectedSecond;
        public DateTime SelectedSecond
        {
            get { return selectedSecond; }
            set
            {
                if (selectedSecond != value)
                {
                    selectedSecond = value;
                    if (SelectedSecond < SelectedFirst)
                        ErrorMessage = "Must select end date greater than start date!";
                    else
                        ErrorMessage = "";
                    OnPropertyChanged(nameof(SelectedSecond));
                }
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }
        public RelayCommand GenerateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        #endregion

        #region actions
        private bool CanExecute_NavigateCommand()
        {
            return true;
        }

        private void Execute_CancelCommand()
        {
            Window window = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (window != null)
            {
                window.Close();
            }
        }

        private void Execute_GenerateCommand()
        {
            AppointmentService appointmentService = new AppointmentService();
            GuideService guideService = new GuideService();
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            string projectFolderPath = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(projectFolderPath, "Resources");
            string filePath = System.IO.Path.Combine(folderPath, "ReportOnScheduledTours.pdf");
            List<string> data = new List<string>();
            data.Add("\n\nNumber of scheduled tours: " + appointmentService.GetScheduledToursForPeriod(SelectedFirst,SelectedSecond).Count());
            SIMS_HCI_Project_Group_5_Team_B.Domain.Models.Guide guide = guideService.getById(appointmentService.GetGuideWithMostTours());
            data.Add("Guide who led the most tours: " + guide.Name + " " + guide.Surname);
            data.Add("The most spoken language: " + appointmentService.GetMostSpokenLanguage());
            
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                document.Open();

                Image agencyImage = Image.GetInstance(folderPath + "/Images/logo.jpeg");
                agencyImage.ScaleToFit(70f, 70f); // Adjust the size as needed
                agencyImage.Alignment = Image.TEXTWRAP | Image.ALIGN_LEFT;
                agencyImage.IndentationLeft = 9f;
                agencyImage.SpacingAfter = 9f;
                document.Add(agencyImage);

                iTextSharp.text.Paragraph par = new iTextSharp.text.Paragraph("Tourist agency\n     Uspon", new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD));
                par.Alignment = Element.ALIGN_LEFT;
                document.Add(par);

                iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph("\nReport for scheduled tours in periof from " + SelectedFirst.ToString("d") + " to " + SelectedSecond.ToString("d"), new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                Paragraph dataParagraph = new Paragraph();
                foreach (string item in data)
                {
                    dataParagraph.Add(new Chunk(item + "\n", new Font(Font.FontFamily.HELVETICA, 12)));
                }
                dataParagraph.Add("\n");
                document.Add(dataParagraph);

                iTextSharp.text.Paragraph text = new iTextSharp.text.Paragraph("\nScheduled tours\n\n", new Font(Font.FontFamily.HELVETICA, 18));
                text.Alignment = Element.ALIGN_CENTER;
                document.Add(text);

                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100f;

                for (int i = 0; i < 1; i++)
                {
                    table.AddCell(new PdfPCell(new Phrase("Tour name", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD))));
                    table.AddCell(new PdfPCell(new Phrase("Language", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD))));
                    table.AddCell(new PdfPCell(new Phrase("Location", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD))));
                    table.AddCell(new PdfPCell(new Phrase("Maximum guests", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD))));
                }

                List<Appointment> all = appointmentService.GetScheduledToursForPeriod(SelectedFirst, SelectedSecond);
                foreach(Appointment appointment in all)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        table.AddCell(new PdfPCell(new Phrase(appointment.Tour.Name, new Font(Font.FontFamily.HELVETICA, 10))));
                        table.AddCell(new PdfPCell(new Phrase(appointment.Tour.Language, new Font(Font.FontFamily.HELVETICA, 10))));
                        table.AddCell(new PdfPCell(new Phrase(appointment.Tour.Location.ToString(), new Font(Font.FontFamily.HELVETICA, 10))));
                        table.AddCell(new PdfPCell(new Phrase(appointment.Tour.MaxGuests.ToString(), new Font(Font.FontFamily.HELVETICA, 10))));
                    }
                }

                document.Add(table);

                iTextSharp.text.Paragraph agencyParagraph = new iTextSharp.text.Paragraph("\n\n\nReport generated on: \n" + DateTime.Now.ToString(), new Font(Font.FontFamily.HELVETICA, 13));
                agencyParagraph.Alignment = Element.ALIGN_RIGHT;
                document.Add(agencyParagraph);

                Image agencyImage1 = Image.GetInstance(folderPath + "/Images/signature.png");
                agencyImage1.ScaleToFit(130f, 60f); // Adjust the size as needed
                agencyImage1.Alignment = Image.TEXTWRAP | Image.ALIGN_RIGHT;
                agencyImage1.IndentationLeft = 9f;
                agencyImage1.SpacingAfter = 9f;
                document.Add(agencyImage1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF: " + ex.Message);
            }
            finally
            {
                document.Close();
            }
            //System.Diagnostics.Process.Start("IExplore.exe", filePath);
            Process.Start(new ProcessStartInfo { FileName = filePath, UseShellExecute = true });
        }
        #endregion
        public DateForReportViewModel()
        {
            this.CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_NavigateCommand);
            this.GenerateCommand = new RelayCommand(Execute_GenerateCommand, CanExecute_NavigateCommand);

        }
    }
}
