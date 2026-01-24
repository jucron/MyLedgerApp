namespace MyLedgerApp.Application.Properties
{
    public class JwtSettings
    {
        public string Key { get; set; } = null!;
        public int ExpireMinutes { get; set; }
    }
}
