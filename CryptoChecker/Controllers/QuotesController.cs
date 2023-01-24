namespace CryptoChecker.Controllers
{
    public class QuotesController : Controller
    {
        #region Main Methods

        // GET: QuotesController/Details/input
        [HttpPost]
        public ActionResult Details(string input)
        {
            QuotesService api = new();
            QuotesModel quotesModel = api.GetAllQuotes(input);
            return View("Details",quotesModel);
        }

        #endregion
    }
}
