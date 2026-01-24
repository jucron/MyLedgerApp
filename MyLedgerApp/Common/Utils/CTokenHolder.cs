namespace MyLedgerApp.Common.Utils
{
    /// <summary>
    /// The CancellationToken Holder, usually from the request.
    /// </summary>
    public static class CTokenHolder
    {
        
        private static readonly AsyncLocal<CancellationTokenHolder> _current = new();

        private class CancellationTokenHolder
        {
            public CancellationToken Token { get; set; }
        }

        public static CancellationToken Current => _current.Value?.Token ?? CancellationToken.None;

        public static void Set(CancellationToken token)
        {
            _current.Value = new CancellationTokenHolder { Token = token };
        }

        public static void Clear()
        {
            _current.Value = null!; 
        }
    }

}
