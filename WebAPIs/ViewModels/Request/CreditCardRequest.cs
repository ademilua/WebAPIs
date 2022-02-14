using System.ComponentModel.DataAnnotations;
using WebAPIs.ViewModels.Error;
using WebAPIs.ViewModels.Regex;

namespace WebAPIs.ViewModels.Request
{
    public class CreditCardRequest
    {
        [Required]
        [RegularExpression(RegexConsts.ValidateCardOwnerName, ErrorMessage = ErrorConsts.CardOwnerNameError)]
        public string CardOwner { get; set; } = string.Empty;
        [Required]
        [RegularExpression(RegexConsts.ValidateCardTypes, ErrorMessage = ErrorConsts.CardError)] //Visa | MasterCard | American Express
        public string CardNumber { get; set; } = string.Empty;
        [Required]
        [RegularExpression(RegexConsts.ValidateCardCvc, ErrorMessage = ErrorConsts.CvcError)]
        public string Cvc { get; set; } = string.Empty;
        [Required]
        [RegularExpression(RegexConsts.ValidateExpMonth, ErrorMessage = ErrorConsts.ExpMonthError)]
        public long ExpMonth { get; set; }
        [Required]
        [RegularExpression(RegexConsts.ValidateExpYear, ErrorMessage = ErrorConsts.ExpYearError)]
        public long ExpYear { get; set; }

    }
}
