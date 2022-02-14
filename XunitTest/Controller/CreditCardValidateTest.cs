using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using WebAPIs.BLL.Interface;
using WebAPIs.Controllers;
using WebAPIs.ViewModels.Request;
using Xunit;

namespace XunitTest.Controller
{
    public class CreditCardValidateTest
    {
        #region private variables
        private const string _visaCardNumber = "4242424242424242";
        private const string _masterCardNumber = "5555555555554444";
        private const string _americanExpressCardNumber = "378282246310005";

        private Mock<ICreditCardValidate> _mockCardValidate;

        private CreditCardInfo request = new CreditCardInfo()
        {
            CardOwner = "Tolulope Ademilua",
            CardNumber = _visaCardNumber,
            Cvc = "123",
            ExpMonth = 8,
            ExpYear = 2025
        };
        #endregion
        #region constructor
        public CreditCardValidateTest()
        {
            _mockCardValidate = new Mock<ICreditCardValidate>();
        }
        #endregion
        #region public
        [Fact]
        public void Visa_CreditCardValidate_Success()
        {
            //Arrange
            request.CardNumber = _masterCardNumber;
            _mockCardValidate.Setup(x => x.ValidateCreditCardInfo(request)).Returns("Visa");

            var controller = GetCreditCardValidateController();

            //Act
            var result = controller.Post(request);

            //Assert
            Assert.IsType<OkObjectResult>(result as OkObjectResult);
        }

        [Fact]
        public void Master_CreditCardValidate_Success()
        {
            //Arrange
            request.CardNumber = _masterCardNumber;
            _mockCardValidate.Setup(x => x.ValidateCreditCardInfo(request)).Returns("MasterCard");
            var controller = GetCreditCardValidateController();

            //Act
            var result = controller.Post(request);
            //Assert
            Assert.IsType<OkObjectResult>(result as OkObjectResult);
        }
        [Fact]
        public void AmericanExpress_CreditCardValidate_Success()
        {
            //Arrange
            request.CardNumber = _americanExpressCardNumber;
            _mockCardValidate.Setup(x => x.ValidateCreditCardInfo(request)).Returns("American Express");
            var controller = GetCreditCardValidateController();

            //Act
            var result = controller.Post(request);

            //Assert
            Assert.IsType<OkObjectResult>(result as OkObjectResult);

        }
        [Fact]
        public void CreditCardValidate_Empty_Cvc_Should_Fail()
        {

            //Arrange
            request.Cvc = String.Empty;
            var controller = GetCreditCardValidateController();
            controller.ModelState.AddModelError("cvc", "The Cvc field is required.");

            //Act
            var result = controller.Post(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);

        }
        [Fact]
        public void CreditCardValidate_Empty_CardOwnerName_Should_Fail()
        {
            //Arrange
            request.CardOwner = String.Empty;
            var controller = GetCreditCardValidateController();
            controller.ModelState.AddModelError("Name", "The Name field is required.");

            //Act
            var result = controller.Post(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        [Fact]
        public void CreditCardValidate_Empty_CardNumber_Should_Fail()
        {
            //Arrange
            request.CardNumber = String.Empty;
            var controller = GetCreditCardValidateController();
            controller.ModelState.AddModelError("CardNumber", "The CardNumber field is required.");

            //Act
            var result = controller.Post(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }

        [Fact]
        public void CreditCardValidate_Invalid_ExpYear_Year_Should_Fail()
        {
            //Arrange
            request.ExpYear = 2020;
            var controller = GetCreditCardValidateController();
            controller.ModelState.AddModelError("ExpYear", "Your card's expiration year is invalid");

            //Act
            var result = controller.Post(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        [Fact]
        public void CreditCardValidate_Invalid_ExpMonth_Should_Fail()
        {
            //Arrange
            request.ExpMonth = 18;
            var controller = GetCreditCardValidateController();
            controller.ModelState.AddModelError("ExpMonth", "Your card's expiration month is invalid");

            //Act
            var result = controller.Post(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        [Fact]
        public void CreditCardValidate_Invalid_CardNumber_Should_Fail()
        {
            //Arrange
            request.CardNumber = "424242";
            var controller = GetCreditCardValidateController();
            controller.ModelState.AddModelError("CardNumber", "The card number is not a valid credit card number");

            //Act
            var result = controller.Post(request);


            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        [Fact]
        public void CreditCardValidate_Invalid_Cvc_Should_Fail()
        {
            //Arrange
            request.Cvc = "12311111";
            var controller = GetCreditCardValidateController();
           
            controller.ModelState.AddModelError("Cvc", "Your card's security code is invalid");


            //Act
            var result = controller.Post(request);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result as BadRequestObjectResult);
        }
        #endregion
        #region private
        private CreditCardValidateController GetCreditCardValidateController()
        {
            return new CreditCardValidateController(_mockCardValidate.Object);
        }
        #endregion
    }
}
