// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoChecker.Services
{
    public class QuotesService : ControllerBase
    {
        #region Variables

        private readonly QuotesModel _quotes;

        #endregion

        #region Constructors

        public QuotesService()
        {
            _quotes = new();
        }

        #endregion

        #region Main Methods

        public QuotesModel GetAllQuotes(string input)
        {
            string[] converts = Resource.converts.Split(",").ToArray();
            foreach (var convert in converts)
            {
                _quotes.Currency.Add(GetQuotesAsync(input, convert));
            }
            return _quotes;
        }

        #endregion

        #region Helper Methods



        private Currency GetQuotesAsync(string input, string convert)
        {
            _quotes.Symbol = input;
            var URL = new UriBuilder(Resource.QuotesURL);
            string json = string.Empty;

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = input;
            queryString["convert"] = convert;

            URL.Query = queryString.ToString();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", Resource.API_Key);
            client.DefaultRequestHeaders.Add("Accepts", "application/json");
            HttpResponseMessage response = client.GetAsync(URL.ToString()).Result;
            response.EnsureSuccessStatusCode();
            json = response.Content.ReadAsStringAsync().Result;
            return JsonToCurrency(json, convert);
        }

        private Currency JsonToCurrency(string json, string convert)
        {
            Currency? currency = new();

            JObject jsonObj = JObject.Parse(json);
            decimal price;

            _quotes.Name = _quotes.Name is null ? jsonObj["data"]!["1"]!["name"]!.ToString() : _quotes.Name;

            Decimal.TryParse(jsonObj["data"]?["1"]?["quote"]?[convert]?["price"]?.ToString(), out price);
            currency.Name = convert;
            currency!.Price = price;
            return currency;
        }

        #endregion
    }
}
