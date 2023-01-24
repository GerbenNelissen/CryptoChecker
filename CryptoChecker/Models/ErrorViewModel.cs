namespace CryptoChecker.Models
{
    public class ErrorViewModel
    {
        #region Variables

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        #endregion
    }
}