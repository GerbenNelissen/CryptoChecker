namespace CryptoChecker.Models
{
    public class QuotesModel
    {
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public List<Currency> Currency { get; set; } = new();
    }
}
