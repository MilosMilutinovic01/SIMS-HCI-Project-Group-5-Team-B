using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SIMS_HCI_Project_Group_5_Team_B.Localization;

namespace SIMS_HCI_Project_Group_5_Team_B
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public void ChangeLanguage(string lang)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo(lang);
        }
        public ResourceDictionary ThemeDictionary
        {
            // You could probably get it via its name with some query logic as well.
            get { return Resources.MergedDictionaries[0]; }
        }

        public void ChangeTheme(Uri uri)
        {
            ThemeDictionary.MergedDictionaries.Clear();
            ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }
    }
}
