namespace WebAPIs.ViewModels.Regex
{
    public class RegexConsts
    {
        public const string ValidateCardOwnerName = @"^[\p{L} \.'\-]+$";
        public const string ValidateCardTypes = @"^4[0-9]{12}(?:[0-9]{3})?$|^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$|^3[47][0-9]{13}$";
        public const string ValidateCardCvc = @"^\d{3}$";
        public const string ValidateExpMonth = @"^(0?[1-9]|1[012])$";
        public const string ValidateExpYear = @"^20[2-9]{2}$";
        public const string ValidateVisaCardNumber = @"^4[0-9]{12}(?:[0-9]{3})?$";
        public const string ValidateMasterCardNumber = @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$";
        public const string ValidateAmericanExpressCardNumber = @"^3[47][0-9]{13}$";
    }
}
