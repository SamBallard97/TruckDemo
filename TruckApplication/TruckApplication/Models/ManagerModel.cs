namespace TruckApplication.Models
{
    public class ManagerModel : PersonModel
    {
        public int PeopleManaged { get; set; }
        public string HeadOffice { get; set; }
    }
}
