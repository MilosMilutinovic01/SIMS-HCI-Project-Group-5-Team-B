using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ReviewsViewModel
    {
        public ObservableCollection<Card> Cards { get; set; }

        public ReviewsViewModel()
        {

            Cards = new ObservableCollection<Card>();
            Cards.Add(new Card("Uros Nikolovski","Tura 1","Ledinacko jezero",5,"Svaka cast gosn!",false,false));
            Cards.Add(new Card("Jelena Kovac","Tura 2","Popovicko jezero",3, "fdsaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\naaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa!", false,false));
            Cards.Add(new Card("Nina Kuzminac","Tura 3","Knicko jezero",4,"sdasaf!",false,false));
        }
    }
}
