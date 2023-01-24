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
            List<Currency> list = new();
            foreach (var convert in converts)
            {
                list.Add(GetQuotes(input, convert));
            }
            _quotes.Currency = list;
            return _quotes;
        }

        #endregion

        #region Helper Methods

        private Currency GetQuotes(string input, string convert)
        {
            var URL = new UriBuilder(Resource.QuotesURL);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbol"] = input;
            queryString["convert"] = convert;

            URL.Query = queryString.ToString();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", Resource.API_Key);
            client.DefaultRequestHeaders.Add("Accepts", "application/json");
            HttpResponseMessage response = client.GetAsync(URL.ToString()).Result;
            response.EnsureSuccessStatusCode();
            string json = response.Content.ReadAsStringAsync().Result;
            return JsonToCurrency(json, convert, input);
        }

        private Currency JsonToCurrency(string json, string convert, string input)
        {
            Currency? currency = new();

            var jsonObject = (JObject)JsonConvert.DeserializeObject(json)!;
            var jsonData = (JObject)(jsonObject.Property("data")!.Value);
            JArray jsonSym = (JArray)jsonData.Property(input.ToUpper())!.Value;
            foreach(JObject item in jsonSym.Children())
            {
                if(item.Property("name") is not null)
                {
                    _quotes.Name = _quotes.Name == String.Empty ? item.Property("name")!.Value.ToString() : _quotes.Name;
                }
                if(item.Property("symbol") is not null)
                {
                    _quotes.Symbol = _quotes.Symbol == String.Empty ? item.Property("symbol")!.Value.ToString() : _quotes.Symbol;
                }
            }
            JObject jsonQuote = new();
            foreach (JObject item in jsonSym.Children())
            {
                if (item.Property("quote") is not null)
                {
                    jsonQuote = (JObject)item.Property("quote")!.Value;
                }
            }
            var jsonConvert = (JObject)jsonQuote.Property(convert)!.Value;
            var jsonPrice = jsonConvert.Property("price")!.Value;

            _ = decimal.TryParse(jsonPrice.ToString(), out decimal price);
            currency.Name = convert;
            currency!.Price = price;            

            return currency;
        }

        #endregion
    }
}
