using System.Text.RegularExpressions;
using WebAPIs.BLL.Interface;
using WebAPIs.ViewModels.Request;
using WebAPIs.ViewModels.Regex;

namespace WebAPIs.BLL
{
    public class CreditCardValidate : ICreditCardValidate
    {
        public string ValidateCreditCardInfo(CreditCardInfo cardInfo)
        {
            var visaCard = new Regex(RegexConsts.ValidateVisaCardNumber);
            var americanExpress = new Regex(RegexConsts.ValidateAmericanExpressCardNumber);
            var masterCard = new Regex(RegexConsts.ValidateMasterCardNumber);
            {

                if (masterCard.IsMatch(cardInfo.CardNumber))
                {
                    return "MasterCard";
                }
                else if (visaCard.IsMatch(cardInfo.CardNumber))
                {
                    return "Visa";

                }
                else if (americanExpress.IsMatch(cardInfo.CardNumber))
                {
                    return "American Express";
                }
                return String.Empty;
            }
        }
    }
}
