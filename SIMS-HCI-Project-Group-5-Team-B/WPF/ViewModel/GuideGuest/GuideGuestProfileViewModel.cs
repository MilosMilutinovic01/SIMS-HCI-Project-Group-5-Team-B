using LiveCharts;
using LiveCharts.Wpf;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class GuideGuestProfileViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public ObservableCollection<SpecialTourRequest> SpecialTourRequests { get; set; }
        public ObservableCollection<GuideGuestTourAttendanceDTO> TourAttendances { get; set; }


        public ObservableCollection<string> YearsWithTourRequests { get; set; }
        private ObservableCollection<string> languageLabels;
        public ObservableCollection<string> LanguageLabels
        {
            get => languageLabels;
            set
            {
                languageLabels = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> locationLabels;
        public ObservableCollection<string> LocationLabels
        {
            get => locationLabels;
            set
            {
                locationLabels = value;
                OnPropertyChanged();
            }
        }
        private SeriesCollection languageSeries;
        public SeriesCollection LanguageSeries
        {
            get => languageSeries;
            set
            {
                languageSeries = value;
                OnPropertyChanged();
            }
        }
        private SeriesCollection locationseries;
        public SeriesCollection LocationSeries
        {
            get => locationseries;
            set
            {
                locationseries = value;
                OnPropertyChanged();
            }
        }
        public Func<string, string> Values { get; set; }



        private string selectedYear;
        public string SelectedYear
        {
            get => selectedYear;
            set
            {
                if(selectedYear != value)
                {
                    selectedYear = value;
                    UpdateChartData();
                    OnPropertyChanged();
                }
            }
        }


        #region RegularTourRequestForm variables
        private TourRequest backupTourRequest;
        private TourRequest selectedTourRequest;
        public TourRequest SelectedTourRequest
        {
            get => selectedTourRequest;
            set
            {
                if (selectedTourRequest != value)
                {
                    selectedTourRequest = value;
                    ShowRegularTourRequestForm = false;
                    if(value != null)
                    {
                        RegularTourSelectedState = selectedTourRequest.Location.State;
                        RegularTourSelectedCity = selectedTourRequest.Location.City;
                    }
                    else
                    {
                        RegularTourSelectedState = string.Empty;
                        RegularTourSelectedCity = string.Empty;
                    }
                    OnPropertyChanged();
                }
            }
        }
        private bool showRegularTourRequestForm;
        public bool ShowRegularTourRequestForm
        {
            get => showRegularTourRequestForm;
            set
            {
                if(showRegularTourRequestForm != value)
                {
                    showRegularTourRequestForm = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand EditRegularTourRequestCommand { get; }
        public ICommand AddNewRegularTourRequestCommand { get; }
        public ICommand SaveRegularTourRequestCommand { get; }
        public ICommand CancelRegularTourRequestCommand { get; }
        #endregion
        #region SpecialTourRequestForm variables
        private SpecialTourRequest backupSpecialTourRequest;
        private SpecialTourRequest selectedSpecialTourRequest;
        public SpecialTourRequest SelectedSpecialTourRequest
        {
            get => selectedSpecialTourRequest;
            set
            {
                if(selectedSpecialTourRequest != value)
                {
                    selectedSpecialTourRequest = value;
                    OnPropertyChanged();
                }
            }
        }
        private TourRequest selectedPart;
        public TourRequest SelectedPart
        {
            get => selectedPart;
            set
            {
                if(selectedPart != value)
                {
                    selectedPart = value;
                    if (value == null)
                    {
                        selectedPart = new TourRequest();
                    }
                    string cityCopy = string.Copy(selectedPart.Location.City);
                    if(string.IsNullOrWhiteSpace(selectedPart.Location.State))
                    {
                        SpecialTourSelectedState = null;
                    }
                    else
                    {
                        SpecialTourSelectedState = selectedPart.Location.State;
                    }
                    if(string.IsNullOrWhiteSpace(cityCopy))
                    {
                        SpecialTourSelectedCity = string.Empty;
                    }
                    else
                    {
                        SpecialTourSelectedCity = cityCopy;
                    }
                    OnPropertyChanged();
                }
            }
        }
        private bool showSpecialTourRequestForm;
        public bool ShowSpecialTourRequestForm
        {
            get => showSpecialTourRequestForm;
            set
            {
                if(showSpecialTourRequestForm != value)
                {
                    showSpecialTourRequestForm = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand AddNewPartCommand { get; }
        public ICommand RemovePartCommand { get; }
        public ICommand EditSpecialTourRequestCommand { get; }
        public ICommand AddNewSpecialTourRequestCommand { get; }
        public ICommand SaveSpecialTourRequestCommand { get; }
        public ICommand CancelSpecialTourRequestCommand { get; }
        #endregion
        #region Location variables for regular and special tour requests
        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> RegularTourCities { get; set; }
        public ObservableCollection<string> SpecialTourCities { get; set; }
        private string regularTourSelectedState = string.Empty;
        public string RegularTourSelectedState
        {
            get => regularTourSelectedState;
            set
            {
                regularTourSelectedState = value;
                RegularTourCities.Clear();
                foreach (var city in locationService.GetCityByState(regularTourSelectedState))
                {
                    RegularTourCities.Add(city);
                }
                OnPropertyChanged();
            }
        }
        private string regularTourSelectedCity = string.Empty;
        public string RegularTourSelectedCity
        {
            get => regularTourSelectedCity;
            set
            {
                if (regularTourSelectedCity != value)
                {
                    regularTourSelectedCity = value;
                    OnPropertyChanged();
                }
            }
        }
        private string specialTourSelectedState = string.Empty;
        public string SpecialTourSelectedState
        {
            get => specialTourSelectedState;
            set
            {
                specialTourSelectedState = value;
                SpecialTourCities.Clear();
                foreach (var city in locationService.GetCityByState(specialTourSelectedState))
                {
                    SpecialTourCities.Add(city);
                }
                SelectedPart.Location.State = specialTourSelectedState;
                OnPropertyChanged("SelectedPart.Location.State");
                OnPropertyChanged();
            }
        }
        private string specialTourSelectedCity = string.Empty;
        public string SpecialTourSelectedCity
        {
            get => specialTourSelectedCity;
            set
            {
                if (specialTourSelectedCity != value)
                {
                    specialTourSelectedCity = value;
                    SelectedPart.Location.City = specialTourSelectedCity;
                    OnPropertyChanged("SelectedPart");
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public SIMS_HCI_Project_Group_5_Team_B.Domain.Models.GuideGuest LoggedGuideGuest { get; set; }

        public ICommand GeneratePDFReportCommand { get; }
        
        private TourRequestService tourRequestService;
        private TourRequestStatisticsService tourRequestStatisticsService;
        private GuideGuestService guideGuestService;
        private LocationService locationService;
        public GuideGuestProfileViewModel()
        {
            tourRequestService = new TourRequestService();
            tourRequestStatisticsService = new TourRequestStatisticsService();
            guideGuestService = new GuideGuestService();
            locationService = new LocationService();

            LoggedGuideGuest = guideGuestService.getLoggedGuideGuest();

            Vouchers = new ObservableCollection<Voucher>(new VoucherService().GetAllFor(LoggedGuideGuest.Id));
            TourRequests = new ObservableCollection<TourRequest>(tourRequestService.GetFor(LoggedGuideGuest.Id));
            SpecialTourRequests = new ObservableCollection<SpecialTourRequest>(new SpecialTourRequestService().GetFor(LoggedGuideGuest.Id));
            TourAttendances = new ObservableCollection<GuideGuestTourAttendanceDTO>(new TourAttendanceService().GetAllAttendances(LoggedGuideGuest.Id));
            LoadYearsWithTourRequests();


            States = new ObservableCollection<string>(locationService.GetStates());
            RegularTourCities = new ObservableCollection<string>();
            SpecialTourCities = new ObservableCollection<string>();


            EditRegularTourRequestCommand = new RelayCommand(EditRegularTourRequest_Execute, CanEditRegularTourRequest);
            AddNewRegularTourRequestCommand = new RelayCommand(AddNewRegularTourRequest_Execute);
            SaveRegularTourRequestCommand = new RelayCommand(SaveRegularTourRequest_Execute, CanSaveRegularTourRequest);
            CancelRegularTourRequestCommand = new RelayCommand(CancelRegularTourRequest_Execute);

            EditSpecialTourRequestCommand = new RelayCommand(EditSpecialTourRequest_Execute, CanEditSpecialTourRequest);
            AddNewSpecialTourRequestCommand = new RelayCommand(AddNewSpecialTourRequest_Execute);
            SaveSpecialTourRequestCommand = new RelayCommand(SaveSpecialTourRequest_Execute, CanSaveSpecialTourRequest);
            CancelSpecialTourRequestCommand = new RelayCommand(CancelSpecialTourRequest_Execute);
            AddNewPartCommand = new RelayCommand(AddNewPart_Execute);
            RemovePartCommand = new RelayCommand(RemovePart_Execute, CanRemovePart);

            GeneratePDFReportCommand = new RelayCommand(GeneratePDFReport_Execute);
        }


        private void LoadYearsWithTourRequests()
        {
            YearsWithTourRequests = new ObservableCollection<string>();
            foreach(var year in tourRequestStatisticsService.GetYearsWithRequests(LoggedGuideGuest.Id))
            {
                YearsWithTourRequests.Add(year.ToString());
            }
            YearsWithTourRequests.Add("Show all years");
        }
        private void UpdateChartData()
        {
            if (SelectedYear == null) return;
            List<TourRequestLanguageStatistics> languageStatistics;
            List<TourRequestLocationStatistics> locationStatistics;
            if(SelectedYear != "Show all years")
            {
                languageStatistics = tourRequestStatisticsService.CalculateLanguageStatistics(LoggedGuideGuest.Id, int.Parse(SelectedYear));
                locationStatistics = tourRequestStatisticsService.CalculateLocationStatistics(LoggedGuideGuest.Id, int.Parse(SelectedYear));
            }
            else
            {
                languageStatistics = tourRequestStatisticsService.CalculateLanguageStatistics(LoggedGuideGuest.Id);
                locationStatistics = tourRequestStatisticsService.CalculateLocationStatistics(LoggedGuideGuest.Id);
            }

            if (LanguageLabels == null)
            {
                LanguageLabels = new ObservableCollection<string>();
                LocationLabels = new ObservableCollection<string>();
            }
            LanguageLabels.Clear();
            LocationLabels.Clear();

            LanguageSeries = new SeriesCollection{
                new StackedColumnSeries
                {
                    Title = "Accepted requests",
                    Values = new ChartValues<int>()
                }
            };
            LanguageSeries.Add(
                new StackedColumnSeries
                {
                    Title = "Rejected requests",
                    Values = new ChartValues<int>()
                });
            
            LocationSeries = new SeriesCollection{
                new StackedColumnSeries
                {
                    Title = "Accepted requests",
                    Values = new ChartValues<int>()
                }
            };
            LocationSeries.Add(
                new StackedColumnSeries
                {
                    Title = "Rejected requests",
                    Values = new ChartValues<int>()
                });
            Values = value => value.ToString();

            foreach (var stat in languageStatistics)
            {
                LanguageSeries[0].Values.Add(stat.NumberOfAcceptedRequests);
                LanguageSeries[1].Values.Add(stat.NumberOfRejectedRequests);
                LanguageLabels.Add(stat.Language);
            }
            foreach (var stat in locationStatistics)
            {
                LocationSeries[0].Values.Add(stat.NumberOfAcceptedRequests);
                LocationSeries[1].Values.Add(stat.NumberOfRejectedRequests);
                LocationLabels.Add(stat.Location.ToString());
            }
        }

        
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //Commands
        private bool CanEditRegularTourRequest()
        {
            return SelectedTourRequest != null;
        }
        private void EditRegularTourRequest_Execute()
        {
            if (ShowSpecialTourRequestForm)
            {
                CancelSpecialTourRequest_Execute();
            }
            backupTourRequest = new TourRequest();
            backupTourRequest.LocationId = SelectedTourRequest.LocationId;
            backupTourRequest.Location = new Location();
            backupTourRequest.Location.City = SelectedTourRequest.Location.City;
            backupTourRequest.Location.State = SelectedTourRequest.Location.State;
            backupTourRequest.Description = SelectedTourRequest.Description;
            backupTourRequest.Language = SelectedTourRequest.Language;
            backupTourRequest.MaxGuests = SelectedTourRequest.MaxGuests;
            backupTourRequest.DateRangeStart = SelectedTourRequest.DateRangeStart;
            backupTourRequest.DateRangeEnd = SelectedTourRequest.DateRangeEnd;

            ShowRegularTourRequestForm = true;
        }
        private void AddNewRegularTourRequest_Execute()
        {
            if (ShowSpecialTourRequestForm)
            {
                CancelSpecialTourRequest_Execute();
            }
            backupTourRequest = null;
            SelectedTourRequest = new TourRequest();
            SelectedTourRequest.AcceptedTourId = -1;
            SelectedTourRequest.SpecialTourId = -1;
            ShowRegularTourRequestForm = true;
        }
        private void CancelRegularTourRequest_Execute()
        {
            if(backupTourRequest != null)
            {
                SelectedTourRequest.LocationId = backupTourRequest.LocationId ;
                SelectedTourRequest.Location.City = backupTourRequest.Location.City;
                SelectedTourRequest.Location.State = backupTourRequest.Location.State;
                SelectedTourRequest.Description = backupTourRequest.Description ;
                SelectedTourRequest.Language = backupTourRequest.Language ;
                SelectedTourRequest.MaxGuests = backupTourRequest.MaxGuests ;
                SelectedTourRequest.DateRangeStart = backupTourRequest.DateRangeStart ;
                SelectedTourRequest.DateRangeEnd = backupTourRequest.DateRangeEnd ;
            }
            backupTourRequest = null;
            SelectedTourRequest = null;
            ShowRegularTourRequestForm = false;
        }
        private bool CanSaveRegularTourRequest()
        {
            return SelectedTourRequest != null && SelectedTourRequest.IsValid;
        }
        private void SaveRegularTourRequest_Execute()
        {
            SelectedTourRequest.LocationId = locationService.GetLocation(RegularTourSelectedState, RegularTourSelectedCity).Id;
            SelectedTourRequest.Location.State = RegularTourSelectedState;
            SelectedTourRequest.Location.City = RegularTourSelectedCity;
            SelectedTourRequest.SpecialTourId = -1;
            if(backupTourRequest == null)
            {
                tourRequestService.Save(SelectedTourRequest);
                TourRequests.Add(SelectedTourRequest);
            }
            else
            {
                tourRequestService.Update(SelectedTourRequest);
            }
            ShowRegularTourRequestForm = false;
        }
        

        private bool CanEditSpecialTourRequest()
        {
            return SelectedSpecialTourRequest != null;
        }
        private void EditSpecialTourRequest_Execute()
        {
            if (ShowRegularTourRequestForm)
            {
                CancelRegularTourRequest_Execute();
            }
            throw new NotImplementedException();
        }
        private void AddNewSpecialTourRequest_Execute()
        {
            if(ShowRegularTourRequestForm)
            {
                CancelRegularTourRequest_Execute();
            }
            backupSpecialTourRequest = null;
            SelectedSpecialTourRequest = new SpecialTourRequest();
            ShowSpecialTourRequestForm = true;
        }
        private void CancelSpecialTourRequest_Execute()
        {
            if(backupSpecialTourRequest != null)
            {
                //Load backup
            }
            ShowSpecialTourRequestForm = false;
            backupSpecialTourRequest = null;
        }
        private bool CanSaveSpecialTourRequest()
        {
            if(SelectedSpecialTourRequest != null)
            {
                foreach (var request in SelectedSpecialTourRequest.TourRequests)
                {
                    if (!request.IsValid) return false;
                }
                return SelectedSpecialTourRequest.IsValid;
            }
            return false;
        }
        private void SaveSpecialTourRequest_Execute()
        {
            SpecialTourRequestService specialTourRequestService = new SpecialTourRequestService();
            specialTourRequestService.Save(SelectedSpecialTourRequest);
            SelectedSpecialTourRequest = specialTourRequestService.Get(SelectedSpecialTourRequest);
            foreach(var tourReqest in selectedSpecialTourRequest.TourRequests)
            {
                tourReqest.SpecialTourId = selectedSpecialTourRequest.Id;
                tourReqest.Location = locationService.GetLocation(tourReqest.Location.State, tourReqest.Location.City);
                tourReqest.LocationId = tourReqest.Location.Id;
                tourRequestService.Save(tourReqest);
            }
            SpecialTourRequests.Add(SelectedSpecialTourRequest);
            ShowSpecialTourRequestForm = false;
        }
        private bool CanRemovePart()
        {
            return SelectedPart != null;
        }
        private void RemovePart_Execute()
        {
            SelectedSpecialTourRequest.TourRequests.Remove(SelectedPart);
            SelectedPart = null;
        }
        private void AddNewPart_Execute()
        {
            SelectedPart = new TourRequest();
            SelectedPart.GuideGuestId = LoggedGuideGuest.Id;
            SelectedPart.AcceptedTourId = -1;
            SelectedPart.Description = "Test";
            SelectedPart.Status = TourRequestStatuses.WAITING;

            SelectedSpecialTourRequest.TourRequests.Add(SelectedPart);
        }
        
        
        private void GeneratePDFReport_Execute()
        {
            TourAttendanceService tourAttendanceService = new TourAttendanceService();
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            string projectFolderPath = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(projectFolderPath, "Resources");
            string filePath = System.IO.Path.Combine(folderPath, "GuideGuestReportOnTourAttendances.pdf");
            
            List<string> data = new List<string>();
            data.Add("\n\n");
            data.Add("Name: " + LoggedGuideGuest.Name);
            data.Add("Surname: " + LoggedGuideGuest.Surname);
            data.Add("Number of tours booked: " + tourAttendanceService.GetNumberOfBookedTour(LoggedGuideGuest.Id));
            data.Add("Number of tour attended: " + tourAttendanceService.GetNumberOfAttendances(LoggedGuideGuest.Id));

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

                iTextSharp.text.Paragraph header = new iTextSharp.text.Paragraph("\nTour attendances report for the last year (from" + DateTime.Now.AddYears(-1).ToString("d") + " to " + DateTime.Now.ToString("d") + ")", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                Paragraph dataParagraph = new Paragraph();
                foreach (string item in data)
                {
                    dataParagraph.Add(new Chunk(item + "\n", new Font(Font.FontFamily.HELVETICA, 12)));
                }
                dataParagraph.Add("\n");
                document.Add(dataParagraph);

                iTextSharp.text.Paragraph text = new iTextSharp.text.Paragraph("\nBooked tours\n\n", new Font(Font.FontFamily.HELVETICA, 18));
                text.Alignment = Element.ALIGN_CENTER;
                document.Add(text);

                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100f;

                for (int i = 0; i < 1; i++)
                {
                    table.AddCell(new PdfPCell(new Phrase("Tour name", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD))));
                    table.AddCell(new PdfPCell(new Phrase("Location", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD))));
                    table.AddCell(new PdfPCell(new Phrase("Language", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD))));
                    table.AddCell(new PdfPCell(new Phrase("People attended", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD))));
                }

                List<GuideGuestTourAttendanceDTO> all = tourAttendanceService.GetAllAttendances(LoggedGuideGuest.Id);
                foreach (GuideGuestTourAttendanceDTO attendanceDTO in all)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        table.AddCell(new PdfPCell(new Phrase(attendanceDTO.Appointment.Tour.Name, new Font(Font.FontFamily.HELVETICA, 10))));
                        table.AddCell(new PdfPCell(new Phrase(attendanceDTO.Appointment.Tour.Location.ToString(), new Font(Font.FontFamily.HELVETICA, 10))));
                        table.AddCell(new PdfPCell(new Phrase(attendanceDTO.Appointment.Tour.Language, new Font(Font.FontFamily.HELVETICA, 10))));
                        table.AddCell(new PdfPCell(new Phrase(attendanceDTO.TourAttendance.PeopleAttending.ToString(), new Font(Font.FontFamily.HELVETICA, 10))));
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
    }
}
