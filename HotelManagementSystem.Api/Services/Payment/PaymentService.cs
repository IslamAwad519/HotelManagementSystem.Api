using HotelManagementSystem.Api.Models;
using Stripe;

namespace HotelManagementSystem.Api.Services.Payment;

public class PaymentService
{
    private readonly IConfiguration _configuration;

    public PaymentService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(Reservation reservation)
    {
        var secretKey = _configuration["StripeSettings:SecretKey"];
        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

        var service = new PaymentIntentService();

        var intent = new PaymentIntent();

        var subtotal = (long)(reservation.TotalAmount * 100);

        if (string.IsNullOrEmpty(reservation.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = subtotal ,
                Currency = "egp",
                PaymentMethodTypes = new List<string> { "card" }
            };
            intent = await service.CreateAsync(options);
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = subtotal 
            };
            await service.UpdateAsync(reservation.PaymentIntentId, options);
        }

        return intent;
    }
}
