namespace MyLedgerApp.Domain
{
    public class Employee: User
    {
        public string bookingCenter = "";
        public DateTime dateCreated = DateTime.UtcNow;
    }
}