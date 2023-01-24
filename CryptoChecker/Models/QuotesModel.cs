namespace CryptoChecker.Models
{
    public class QuotesModel
    {
        #region Variables
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public IEnumerable<Currency>? Currency { get; set; }

        #endregion
    }
}
