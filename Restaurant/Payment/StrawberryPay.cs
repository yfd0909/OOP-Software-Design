using System;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Payment
{
    public class StrawberryPay(string apiKey, string publisher)
    {
        private static readonly Random Random = new();

        public string ApiKey { get; set; } = apiKey;

        public string Publisher { get; } = publisher;

        public async Task<StrawberryPayResponse> Pay(StrawberryPayRequest request)
        {
            await Task.Delay(Random.Next(100, 400));
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new StrawberryPayException("Unknown API key");
            }
            DateTime publishedAt = DateTime.UtcNow;
            Guid id = Guid.CreateVersion7(publishedAt);
            StrawberryPayException? exception = await IsValidCreditInformation(request);
            return exception is null
                ? new StrawberryPaySuccessResponse(id, Publisher, publishedAt, request.Price)
                : new StrawberryPayFailureResponse(id, Publisher, publishedAt, exception);
        }

        private static async Task<StrawberryPayException?> IsValidCreditInformation(StrawberryPayRequest request)
        {
            await Task.Delay(Random.Next(100, 400));
            Optional<uint> creditCardNumberValidator = request.CreditCardNumber
                .AsOptional()
                .Filter(s => !string.IsNullOrWhiteSpace(s))
                .Filter(s => s.Length == 16)
                .Filter(s => s.All(char.IsNumber))
                .Map(s => s.GetStringHashCode());
            Optional<uint> cvcValidator = request.Cvc
                .AsOptional()
                .Filter(s => !string.IsNullOrWhiteSpace(s))
                .Filter(s => s.Length == 3)
                .Filter(s => s.All(char.IsNumber))
                .Map(uint.Parse);
            if (!creditCardNumberValidator.HasValue || !cvcValidator.HasValue)
            {
                return new StrawberryPayException("Invalid format provided");
            }
            return creditCardNumberValidator
                .Merge(cvcValidator, (hashCode, cvcNumber) => hashCode % 1000 == cvcNumber)
                .Filter(b => b)
                .Map<StrawberryPayException?>(_ => null)
                .OrElse(new StrawberryPayException("Unknown credit card number and cvc provided"));
        }
    }

    public record StrawberryPayRequest(
        Guid Id,
        string Publisher,
        DateTime PublishedAt,
        string CreditCardNumber,
        string Cvc,
        double Price);

    public abstract record StrawberryPayResponse(
        Guid Id,
        string Publisher,
        DateTime PublishedAt);

    public record StrawberryPaySuccessResponse(
        Guid Id,
        string Publisher,
        DateTime PublishedAt,
        double Price) : StrawberryPayResponse(Id, Publisher, PublishedAt);

    public record StrawberryPayFailureResponse(
        Guid Id,
        string Publisher,
        DateTime PublishedAt,
        StrawberryPayException Exception) : StrawberryPayResponse(Id, Publisher, PublishedAt);

    public class StrawberryPayException(string? message = null) : Exception(message);
}
