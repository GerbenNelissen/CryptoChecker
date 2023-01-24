namespace CryptoChecker.Tests
{
    public class QuotesServiceTests
    {
        [Fact]
        public void GetAllQuotes_ValidInput_QuotesModel()
        {
            // Arrange
            QuotesModel quotes = new QuotesModel()
            {
                Name = "Bitcoin",
                Symbol = "BTC"
            };

            // Act
            var result = GetInstance().GetAllQuotes("BTC");

            // Assert
            result.Should().BeOfType(typeof(QuotesModel));
            result.Name.Should().Be(quotes.Name);
            result.Symbol.Should().Be(quotes.Symbol);
        }

        [Fact]
        public void GetAllQuotes_InvalidInput_DetailsView()
        {
            // Arrange
            QuotesService controller = GetInstance();

            // Act
            var exception = Assert.Throws<NullReferenceException>(() => controller.GetAllQuotes("asdadasdasfsdf"));

            // Assert
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

        private static QuotesService GetInstance() => new();
    }
}
