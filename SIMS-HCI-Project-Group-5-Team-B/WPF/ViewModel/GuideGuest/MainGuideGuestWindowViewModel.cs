using SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.Pages;
using System;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class MainGuideGuestWindowViewModel
    {
        private GuideGuestProfilePage guideGuestProfilePage;
        private TourInformationPage tourInformationPage;
        private TourSearchPage tourSearchPage;
        
        public MainGuideGuestWindowViewModel()
        {
            guideGuestProfilePage = new GuideGuestProfilePage();
            tourInformationPage = new TourInformationPage();
            tourSearchPage = new TourSearchPage();
        }

        public object GetYourProfilePage()
        {
            return guideGuestProfilePage;
        }

        public object GetTourSearchPage()
        {
            return tourSearchPage;
        }
    }
}
