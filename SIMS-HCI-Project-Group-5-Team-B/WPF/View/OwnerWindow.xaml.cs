using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        
        private LocationController locationService;
        private AccommodationService accommodationService;
        private ReservationService reservationService;
        private OwnerService ownerService;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        private OwnerGuestGradeService ownerGuestGradeService;
        private SuperOwnerService superOwnerService;
        private ReservationChangeRequestService reservationChangeRequestService;
        private IRenovationService renovationService;
        private RenovationRequestService renovationRequestService;
        private YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService;
        private MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService;
        public Owner LogedInOwner { get; set; }
        private string username;
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";
        //private string currentLanguage;
        public OwnerWindow(string username)
        {
            InitializeComponent();
            this.username = username;
            locationService = new LocationController();
            ownerService = new OwnerService();
            accommodationService = new AccommodationService(locationService, ownerService);
            reservationService = new ReservationService();
            ownerAccommodationGradeService = new OwnerAccommodationGradeSevice(reservationService);
            ownerGuestGradeService = new OwnerGuestGradeService(reservationService);
            superOwnerService = new SuperOwnerService(ownerAccommodationGradeService, accommodationService);
            reservationChangeRequestService = new ReservationChangeRequestService();
            renovationService = ServiceInjector.CreateInstance<IRenovationService>();
            renovationRequestService = new RenovationRequestService();
            yearlyAccommodationStatisticsService = new YearlyAccommodationStatisticsService();
            monthlyAccommodationStatisticsService = new MonthlyAccommodationStatisticsService();

            LogedInOwner = ownerService.GetByUsername(username);
            LogedInOwner.GradeAverage = superOwnerService.CalculateGradeAverage(LogedInOwner);

            //OVU FJU DA AZUZIRANJE MOZDA DODATI U SUPEROWNERSERVICE
            if (LogedInOwner.GradeAverage > 4.5 && superOwnerService.GetNumberOfGrades(LogedInOwner) >= 50)
            {
                LogedInOwner.IsSuperOwner = true;
            }
            else
            {
                LogedInOwner.IsSuperOwner = false;
            }

            ownerService.Update(LogedInOwner);

            app = (App)System.Windows.Application.Current;
            if(Properties.Settings.Default.currentLanguage == "en-US")
            {
                LocalizationComboBox.SelectedIndex = 0;
            }
            else
            {
                LocalizationComboBox.SelectedIndex = 1;
            }
            if(Properties.Settings.Default.darkThemeOn == true)
            {
                ThemeToggleButton.IsChecked = true;
            }
            else
            {
                ThemeToggleButton.IsChecked = false;
            }


        }

        private void NotifyOwner(object sender, RoutedEventArgs e)
        {
            frame.Content = new AccommodationPage(LogedInOwner.Id,reservationService);
            List<Reservation> reservationsForGrading = reservationService.GetReservationsForGrading(LogedInOwner);
            if (reservationsForGrading.Count != 0 )
            {
                if(Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("You have guests to grade!!!");
                }
                else
                {
                    MessageBox.Show("Imate goste za ocenjivanje!!!");
                }
            }
     
            NotificationController notificationController = new NotificationController();
            UserController userController = new UserController();
            User user = userController.GetByUsername(username);
            if (notificationController.Exists(user.Id))
            {
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("You have new notifactions!");
                }
                else
                {
                    MessageBox.Show("Imate nove notifikacije!");
                }
            }
        }


        private void Accommodation_Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new AccommodationPage(LogedInOwner.Id,reservationService);
        }

        private void Requests_For_Changing_Reservation_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new RequestsForChangingReservationPage(reservationChangeRequestService,reservationService,LogedInOwner);
        }

        private void Grading_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new GradingPage(reservationService,ownerAccommodationGradeService,ownerGuestGradeService,LogedInOwner);
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new StatisticsPage(yearlyAccommodationStatisticsService,accommodationService,LogedInOwner,monthlyAccommodationStatisticsService);
        }

        private void Renovation_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new RenovationPage(renovationService,reservationService,LogedInOwner,accommodationService);
        }

        private void Owner_Forum_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new OwnerForumPage(LogedInOwner,accommodationService);
        }

        private void Owner_Profile_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new OwnerProfilePage(LogedInOwner,superOwnerService);
        }

        private void Owner_Notifications_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new OwnerNotificationsPage(LogedInOwner);
        }

        private void LocalizationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            ComboBoxItem cbItem = (ComboBoxItem)cBox.SelectedItem;
            string newType = (string)cbItem.Content;
            if (newType == "English")
            {
                Properties.Settings.Default.currentLanguage = ENG;
                app.ChangeLanguage(ENG);
                //currentLanguage = ENG;
                Properties.Settings.Default.Save();
            }
            else if (newType == "Serbian")
            {
                //pristupi i promeni settingsu i toj prom
                Properties.Settings.Default.currentLanguage = SRB;
                app.ChangeLanguage(SRB);
                //currentLanguage = SRB;
                Properties.Settings.Default.Save();
                //trebao bi save iz setingsa
            }
        }
        private void Toggle2_Checked(object sender, RoutedEventArgs e)
        {
            app.ChangeTheme(new Uri("Themes/DarkTheme.xaml", UriKind.Relative));
            Properties.Settings.Default.darkThemeOn = true;
            Properties.Settings.Default.Save();
        }

        private void Toggle2_Unchecked(object sender, RoutedEventArgs e)
        {
            app.ChangeTheme(new Uri("Themes/LightTheme.xaml", UriKind.Relative));
            Properties.Settings.Default.darkThemeOn = false;
            Properties.Settings.Default.Save();
        }
    }
}
