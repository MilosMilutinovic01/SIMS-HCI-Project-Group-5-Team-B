using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for PreviewImageWindow.xaml
    /// </summary>
    public partial class PreviewImageWindow : Window
    {
        public PreviewImageWindow(string imageName)
        {
            InitializeComponent();
            this.DataContext = this;
            string projectDirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

            // Set the image file path relative to the project directory
            string imagePath = System.IO.Path.Combine(projectDirectory, imageName);

            // Create a Uri from the image file path
            Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);

            // Set the source of the Image control
            ImageControl.Source = new BitmapImage(imageUri);
        }
    }
}
