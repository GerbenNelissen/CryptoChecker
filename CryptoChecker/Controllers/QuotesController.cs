namespace CryptoChecker.Controllers
{
    public class QuotesController : Controller
    {
        #region Variables

        public string Name { get; set; } = string.Empty;

        public string Symbol { get; set; } = string.Empty;

        public List<Currency> Currency { get; set; } = new();

        #endregion

        #region Constructor

        #endregion

        #region Main Methods

        // GET: QuotesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuotesController/Details/input
        public ActionResult Details(string input)
        {
            QuotesService api = new();
            QuotesModel quotesModel = api.GetAllQuotes(input);
            QuotesController quotes = new()
            {
                Name = quotesModel.Name,
                Symbol = quotesModel.Symbol,
            };
            foreach(var currency in quotesModel.Currency)
            {
                quotes.Currency.Add(currency);
            }
            return View(quotes);
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}
