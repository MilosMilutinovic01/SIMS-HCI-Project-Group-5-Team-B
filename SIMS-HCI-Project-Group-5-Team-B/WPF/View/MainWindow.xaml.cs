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
        //public KeyPointsController keyPointsController;
        //public LocationController locationController;
        //public TourController tourController;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ComboBoxType.SelectedIndex = 0;

            userController = new UserController();
            //keyPointsController = new KeyPointsController();
            //locationController = new LocationController();
            //locationController.ChangeCsvFile("../../../Resources/Data/Locations.csv");
            //tourController = new TourController(locationController);
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
            



            if(ComboBoxType.SelectedIndex == 0)//Guide is selected
            {
                GuideWindow guideWindow = new GuideWindow(user.Username);
                guideWindow.Show();
                //Pozovi funkciju koju hoces za VODICA

            } else if(ComboBoxType.SelectedIndex == 1)//Guide_Guest is selected
            {

                //Pozovi funkciju koju hoces za GOSTA 2
                TourWindow tourWindow = new TourWindow(user);
                tourWindow.Show();

            }
            else if(ComboBoxType.SelectedIndex == 2)//Owner is selected
            {

                //Pozovi funkciju koju hoces za VLASNIKA
                OwnerWindow ownerWindow = new OwnerWindow(Username);
                ownerWindow.Show();


            }
            else if(ComboBoxType.SelectedIndex == 3)//Owner_Guest is selected
            {

                //Pozovi funkciju koju hoces za GOSTA 1
               OwnerGuestWindow ownerGuestWindow = new OwnerGuestWindow(Username);
                ownerGuestWindow.Show();
            }
        }
    }
}
