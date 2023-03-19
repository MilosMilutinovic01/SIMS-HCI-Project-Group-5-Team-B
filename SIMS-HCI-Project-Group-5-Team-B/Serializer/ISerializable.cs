namespace SIMS_HCI_Project_Group_5_Team_B.Serializer
{
    public interface ISerializable
    {
        public int Id { get; set; }
        string[] ToCSV();
        void FromCSV(string[] values);

    }
}
