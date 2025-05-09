using System;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Payment
{
    public class BananaPaymentGateway(string apiKey)
    {
        private static readonly Random Random = new();

        public string ApiKey { get; set; } = apiKey;

        public async Task<BananaPaymentRequest> CreatePaymentRequest(string creditCardNumber, string cvc, long price)
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new BananaPaymentException("Unknown API key");
            }
            BananaRequestResult requestResult = await IsValidCreditInformation(creditCardNumber, cvc);
            if (requestResult != BananaRequestResult.Success)
            {
                switch (requestResult)
                {
                case BananaRequestResult.Success:
                    break;
                case BananaRequestResult.InvalidFormat:
                    throw new BananaPaymentException("Invalid credit card information provided");
                case BananaRequestResult.UnauthorizedInformation:
                    throw new BananaPaymentException("Unauthorized credit card information provided");
                }
            }
            DateTime publishedAt = DateTime.UtcNow;
            Guid id = Guid.CreateVersion7(publishedAt);
            return new BananaPaymentRequest(id, creditCardNumber, cvc, price);
        }

        public async Task<BananaPaymentResponse> CancelPayment(BananaPaymentRequest request)
        {
            await Task.Delay(Random.Next(100, 1000));
            await IsValidCreditInformation(request.CreditCardNumber, request.Cvc);
            DateTime publishedAt = DateTime.UtcNow;
            return new BananaPaymentResponse(
                request.Id,
                ApiKey,
                request.CreditCardNumber,
                publishedAt,
                request.Price);
        }

        public async Task<BananaPaymentResponse> ProceedPayment(BananaPaymentRequest request)
        {
            await Task.Delay(Random.Next(100, 1000));
            await IsValidCreditInformation(request.CreditCardNumber, request.Cvc);
            DateTime publishedAt = DateTime.UtcNow;
            return new BananaPaymentResponse(
                request.Id,
                ApiKey,
                request.CreditCardNumber,
                publishedAt,
                request.Price);
        }

        private static async Task<BananaRequestResult> IsValidCreditInformation(string creditCardNumber, string cvc)
        {
            await Task.Delay(Random.Next(100, 400));
            Optional<uint> creditCardNumberValidator = Optional.Of(creditCardNumber)
                .Filter(s => !string.IsNullOrWhiteSpace(s))
                .Filter(s => s.Length == 16)
                .Filter(s => s.All(char.IsNumber))
                .Map(s => s.GetStringHashCode());
            Optional<uint> cvcValidator = Optional.Of(cvc)
                .Filter(s => !string.IsNullOrWhiteSpace(s))
                .Filter(s => s.Length == 3)
                .Filter(s => s.All(char.IsNumber))
                .Map(uint.Parse);
            if (!creditCardNumberValidator.HasValue || !cvcValidator.HasValue)
            {
                return BananaRequestResult.InvalidFormat;
            }
            return creditCardNumberValidator
                .Merge(cvcValidator, (hashCode, cvcNumber) => hashCode % 1000 == cvcNumber)
                .Filter(b => b)
                .Map(_ => BananaRequestResult.Success)
                .OrElse(BananaRequestResult.UnauthorizedInformation);
        }

        private enum BananaRequestResult
        {
            Success,
            InvalidFormat,
            UnauthorizedInformation
        }
    }

    public record BananaPaymentRequest(Guid Id, string CreditCardNumber, string Cvc, long Price);

    public record BananaPaymentResponse(
        Guid Id,
        string Publisher,
        string CreditCardNumber,
        DateTime PublishedAt,
        long Price);

    public record BananaPaymentCancellationResponse(
        Guid Id,
        string Publisher,
        string CreditCardNumber,
        DateTime PublishedAt,
        long Price) : BananaPaymentResponse(Id, Publisher, CreditCardNumber, PublishedAt, Price);

    public class BananaPaymentException(string message)
        : Exception(message);
}
