using WebAPIs.ViewModels.Request;

namespace WebAPIs.BLL.Interface
{
    public interface ICreditCardValidate
    {
        string ValidateCreditCardInfo(CreditCardInfo cardInfo);
    }
}
