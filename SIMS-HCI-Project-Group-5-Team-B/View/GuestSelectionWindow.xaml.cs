using SIMS_HCI_Project_Group_5_Team_B.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    public partial class GuestSelectionWindow : Window
    {
        public ObservableCollection<User> Guests { get; set; }
        public static List<User> GuestList;
        public GuestSelectionWindow(List<User> users)
        {
            InitializeComponent();
            DataContext = this;
            GuestList = new List<User>();
            Guests = new ObservableCollection<User>(users.FindAll(u => u.Type == Model.Type.GUIDEGUEST));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(User u in Guests)
            {
                GuestList.Add(u);
            }
            this.Close();
        }
    }
}
