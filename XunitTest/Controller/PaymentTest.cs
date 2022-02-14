using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using WebAPIs.Controllers;
using WebAPIs.Utilities;
using WebAPIs.ViewModels.Request;
using Xunit;

namespace XunitTest.Controller
{
    public class PaymentTest
    {
        #region private variables
        private const string _visaCardNumber = "4242424242424242";
        private const string _masterCardNumber = "5555555555554444";
        private const string _americanExpressCardNumber = "378282246310005";
        private readonly IOptions<AppSettings> _appSetting;
        private readonly ILogger<PaymentController> _logger;
        private AppSettings appSettings = new AppSettings()
        {
            StripeSecretKey = "sk_test_51KRN8TGlLXuAhkL1i1ntKr2Hpe7xnvIhp0YlqCFuY9xHY3C3YxEVxw6W59UbN7fx61LDY6GB0ozmgfnYrJeQDdKw00pT48qzS0"
        };
        private CreditCardRequest request = new CreditCardRequest()
        {
            CardOwner = "Tolulope Ademilua",
            CardNumber = _visaCardNumber,
            Cvc = "123",
            ExpMonth = 8,
            ExpYear = 2025
        };
        #endregion
        #region constructor
        public PaymentTest()
        {
            _appSetting = Options.Create(appSettings);
            var mock = new Mock<ILogger<PaymentController>>();
            _logger = mock.Object;
            _logger = Mock.Of<ILogger<PaymentController>>();
        }
        #endregion
        #region public
        [Fact]
        public async Task Visa_CreditCardValidate_Success()
        { 
            //Arrange
            var controller = new PaymentController(_logger, _appSetting);

            //Act
            var result =await controller.PostAsync(request);
            //Assert
            var assertResult = Assert.IsType<OkObjectResult>(result as OkObjectResult);
            Assert.Equal("Visa", assertResult.Value);
        }

        [Fact]
        public async Task Master_CreditCardValidate_Success()
        {
            //Arrange
            var controller = new PaymentController(_logger, _appSetting);
            request.CardNumber = _masterCardNumber;

            //Act
            var result = await controller.PostAsync(request);
            //Assert
            var assertResult = Assert.IsType<OkObjectResult>(result as OkObjectResult);
            Assert.Equal("MasterCard", assertResult.Value);
        }
        [Fact]
        public async Task AmericanExpress_CreditCardValidate_Success()
        {
            //Arrange
            var controller = new PaymentController(_logger, _appSetting);
            request.CardNumber = _americanExpressCardNumber;

            //Act
            var result = await controller.PostAsync(request);
            //Assert
            var assertResult = Assert.IsType<OkObjectResult>(result as OkObjectResult);
            Assert.Equal("American Express", assertResult.Value);

        }
        [Fact]
        public async Task CreditCardValidate_Empty_Cvc_Should_Fail()
        {

            //Arrange
            request.Cvc = String.Empty;
            var controller = new PaymentController(_logger, _appSetting);
            controller.ModelState.AddModelError("cvc", "The Cvc field is required.");

            //Act
            var result = await controller.PostAsync(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
           
        }
        [Fact]
        public async Task CreditCardValidate_Empty_CardOwnerName_Should_Fail()
        {
            //Arrange
            request.CardOwner = String.Empty;
            var controller = new PaymentController(_logger, _appSetting);
            controller.ModelState.AddModelError("Name", "The Name field is required.");

            //Act
            var result = await controller.PostAsync(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        [Fact]
        public async Task CreditCardValidate_Empty_CardNumber_Should_Fail()
        {
            //Arrange
            request.CardNumber = String.Empty;
            var controller = new PaymentController(_logger, _appSetting);
            controller.ModelState.AddModelError("CardNumber", "The CardNumber field is required.");

            //Act
            var result = await controller.PostAsync(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }

        [Fact]
        public async Task CreditCardValidate_Invalid_ExpYear_Year_Should_Fail()
        {
            //Arrange
            var controller = new PaymentController(_logger, _appSetting);
            request.ExpYear = 2020;

            //Act
            var result = await controller.PostAsync(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        [Fact]
        public async Task CreditCardValidate_Invalid_ExpMonth_Should_Fail()
        {
            //Arrange
            var controller = new PaymentController(_logger, _appSetting);
            request.ExpMonth = 18;

            //Act
            var result = await controller.PostAsync(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        [Fact]
        public async Task CreditCardValidate_Invalid_CardNumber_Should_Fail()
        {
            //Arrange
            request.CardNumber = "424242";
            var controller = new PaymentController(_logger, _appSetting);

            //Act
            var result = await controller.PostAsync(request);


            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        [Fact]
        public async Task CreditCardValidate_Invalid_Cvc_Should_Fail()
        {
            //Arrange
            request.Cvc = "12311111";
            var controller = new PaymentController(_logger, _appSetting);

            //Act
            var result = await controller.PostAsync(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
       #endregion
    }

}
