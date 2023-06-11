using PdfSharp.Pdf;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
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
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for PdfWindow.xaml
    /// </summary>
    public partial class PdfWindow : Window
    {

        public PdfWindow(string filepath)
        {
            InitializeComponent();
            this.DataContext = this;
            System.Uri uri = new System.Uri(filepath);
            webBrowser.Source = uri;
            
        }
    }
}
