namespace CryptoChecker.Tests
{
    public class QuotesControllerTests
    {
        [Fact]
        public void Details_ValidInput_DetailsView()
        {
            // Arrange & Act
            var result = GetInstance().Details("BTC");

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void Details_InvalidInput_DetailsView()
        {
            // Arrange
            QuotesController controller = GetInstance();

            // Act
            var exception = Assert.Throws<NullReferenceException>(() => controller.Details("asdadasdasfsdf"));

            // Assert
            exception.Message.Should().Be("Object reference not set to an instance of an object.");

        }

        private static QuotesController GetInstance() => new();
    }
}