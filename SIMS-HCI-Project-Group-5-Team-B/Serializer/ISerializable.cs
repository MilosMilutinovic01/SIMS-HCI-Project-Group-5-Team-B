namespace SIMS_HCI_Project_Group_5_Team_B.Serializer
{
    public interface ISerializable
    {
        string[] ToCSV();
        void FromCSV(string[] values);

    }
}
