namespace MyLedgerApp.Application.Properties
{
    public class DbSettings
    {
        public string DefaultConnection { get; set; } = null!;
        public bool ShouldMigrate { get; set; }
        public bool ShouldReset { get; set; }
    }
}
