namespace MyLedgerApp.Application.Properties
{
    public interface IAppProperties
    {
        CacheSettings CacheSettings { get; } 
        JwtSettings JwtSettings { get; }
    }
}
