using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using WebAPIs.Utilities;
using WebAPIs.ViewModels.Request;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly AppSettings _appSettings;
        #region constructor
        public PaymentController(ILogger<PaymentController> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            StripeConfiguration.ApiKey = _appSettings.StripeSecretKey;
        }
        #endregion
        #region post
        [HttpPost("CreditCardValidate")]
        public async Task<IActionResult> PostAsync([FromBody] CreditCardRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            var options = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Name = request.CardOwner,
                    Number = request.CardNumber,
                    Cvc = request.Cvc,
                    ExpMonth = request.ExpMonth,
                    ExpYear = request.ExpYear
                },
            };

            var service = new TokenService();
            try
            {
                Token response = await service.CreateAsync(options);
                return Ok(response.Card.Brand);
            }
            catch (StripeException se)
            {
                return BadRequest(se.Message);
            }
        }
        #endregion
    }
}
