using ePizzaHub.Entities;
using ePizzaHub.DAL.Interfaces;
using ePizzaHub.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;
using ePizzaHub.Repositories.Interfaces;

namespace ePizzaHub.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly RazorpayClient _client;
        private readonly IRepository<PaymentDetails> _paymentRepo;
        private readonly IConfiguration _config;
        ICartRepository _cartRepo;
        public PaymentService(IRepository<PaymentDetails> paymentRepo, IConfiguration config, ICartRepository cartRepo)
        {
            _config = config;
            _cartRepo = cartRepo;
            if (_client == null)
            {
                _client = new RazorpayClient(config["RazorPayConfig:Key"], config["RazorPayConfig:Secret"]);
            }
            _paymentRepo = paymentRepo;
        }
        public string CreateOrder(decimal amount, string currency, string receipt)
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", amount);
            options.Add("receipt", receipt);
            options.Add("currency", currency);
            Razorpay.Api.Order order = _client.Order.Create(options);
            return order["id"].ToString();
        }

        public Payment GetPaymentDetails(string paymentId)
        {
            return _client.Payment.Fetch(paymentId);
        }

        public int SavePaymentDetails(PaymentDetails model)
        {
            _paymentRepo.Add(model);
            var cart = _cartRepo.Find(model.CartId);
            cart.IsActive = false;
            return _paymentRepo.SaveChanges();
        }

        public bool VerifySignature(string signature, string orderId, string paymentId)
        {
            string payload = string.Format("{0}|{1}", orderId, paymentId);
            string secret = RazorpayClient.Secret;
            string actualSignature = getActualSignature(payload, secret);
            return actualSignature.Equals(signature);
        }

        private static string getActualSignature(string payload, string secret)
        {
            byte[] secretBytes = StringEncode(secret);
            HMACSHA256 hashHmac = new HMACSHA256(secretBytes);
            var bytes = StringEncode(payload);

            return HashEncode(hashHmac.ComputeHash(bytes));
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
