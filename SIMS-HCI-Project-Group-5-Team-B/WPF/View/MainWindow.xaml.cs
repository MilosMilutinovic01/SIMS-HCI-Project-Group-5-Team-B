using SIMS_HCI_Project_Group_5_Team_B.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using System.IO;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;

namespace SIMS_HCI_Project_Group_5_Team_B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Username { get; set; }
        public string Password { get; set; }
        private UserController userController;
        //public KeyPointsController keyPointsService;
        //public LocationController locationController;
        //public TourController tourController;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            userController = new UserController();
            //keyPointsService = new KeyPointsController();
            //locationController = new LocationController();
            //locationController.ChangeCsvFile("../../../Resources/Data/Locations.csv");
            //tourController = new TourController(locationController);
            Injector.LoadData();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            User user = null;

            if(!Username.Equals("") || !Password.Equals(""))
            {
                user = userController.LogIn(Username, Password);
                if (user == null)
                {
                    MessageBox.Show("Username or password is incorrect");
                    return;
                }
            }
            
            user = userController.LogIn(Username, Password);
            (new UserService()).LogIn(Username, Password);

            if(user.Type == USERTYPE.Guide)//Guide is selected
            {
                Guide guide = new Guide("Milos", "Milutinovic");
                GuideWindow guideWindow = new GuideWindow(guide);
                guideWindow.Show();

            } else if(user.Type == USERTYPE.GuideGuest)//Guide_Guest is selected
            {
                MainGuideGuestWindow mainGuideGuestWindow = new MainGuideGuestWindow();
                mainGuideGuestWindow.Show();
                //Pozovi funkciju koju hoces za GOSTA 2
                //TourWindow tourWindow = new TourWindow(user);
                //tourWindow.Show();

            }
            else if(user.Type == USERTYPE.Owner)//Owner is selected
            {

                //Pozovi funkciju koju hoces za VLASNIKA
                OwnerWindow ownerWindow = new OwnerWindow(Username);
                ownerWindow.Show();


            }
            else if(user.Type == USERTYPE.OwnerGuest)//Owner_Guest is selected
            {

                //Pozovi funkciju koju hoces za GOSTA 1
               // OwnerGuestService ownerGuestService = new OwnerGuestService();
                //OwnerGuestService.LoggedInOwnerGuest = ownerGuestService.GetByUsername(Username);
               OwnerGuestWindow ownerGuestWindow = new OwnerGuestWindow(Username);
                ownerGuestWindow.Show();
            }
        }
    }
}
