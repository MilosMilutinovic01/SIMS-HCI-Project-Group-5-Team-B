using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class SuperOwnerGuestTitleCSVRepository: CSVRepository<SuperOwnerGuestTitle>, ISuperOwnerGuestTitleRepository
    {
        public SuperOwnerGuestTitleCSVRepository() : base() { }

        public void Delete(SuperOwnerGuestTitle superOwnerGuestTitle)
        {
            SuperOwnerGuestTitle? founded = _data.Find(d => d.Id == superOwnerGuestTitle.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public void Save(SuperOwnerGuestTitle superOwnerGuestTitle)
        {
            superOwnerGuestTitle.Id = NextId();
            _data.Add(superOwnerGuestTitle);
            WriteCSV(_data);
        }
        private int NextId()
        {
            if (_data.Count() < 1)
            {
                return 1;
            }
            else
            {
                return _data.Max(d => d.Id) + 1;
            }
        }

        public void Update(SuperOwnerGuestTitle tourGrade)
        {
            SuperOwnerGuestTitle? current = _data.Find(d => d.Id == tourGrade.Id);
            if (current == null)
            {
                Save(tourGrade);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, tourGrade);
            WriteCSV(_data);
        }

        public List<SuperOwnerGuestTitle> GetAll()
        {
            return _data;
        }

        protected override string[] ToCSV(SuperOwnerGuestTitle obj)
        {
            string[] csvValues =
            {
                obj.Id.ToString(),
                obj.OwnerGuestId.ToString(),
                obj.ActivationDate.ToString(),
                obj.AvailablePoints.ToString()
            };
            return csvValues;
        }

        protected override SuperOwnerGuestTitle FromCSV(string[] values)
        {
            SuperOwnerGuestTitle superOwnerGuestTitle = new SuperOwnerGuestTitle();

            superOwnerGuestTitle.Id = int.Parse(values[0]);
            superOwnerGuestTitle.OwnerGuestId = int.Parse(values[1]);
            superOwnerGuestTitle.ActivationDate = DateTime.Parse(values[2]);
            superOwnerGuestTitle.AvailablePoints = int.Parse(values[3]);

            return superOwnerGuestTitle;
        }

       
        public SuperOwnerGuestTitle GetById(int id)
        {
            return _data.Find(sup => sup.Id == id);
        }

        public SuperOwnerGuestTitle GetActiveByOwnerGuestId(int ownerGuestId)
        {
            return _data.Find(sup => sup.OwnerGuestId == ownerGuestId && sup.ActivationDate > DateTime.Today.AddYears(-1));
        }
    }
}
