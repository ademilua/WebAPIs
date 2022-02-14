using Microsoft.AspNetCore.Mvc;
using WebAPIs.BLL.Interface;
using WebAPIs.ViewModels.Request;
namespace WebAPIs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardValidateController : ControllerBase
    {
        private readonly ICreditCardValidate _cardValidate;
        #region constructor
        public CreditCardValidateController(ICreditCardValidate cardValidate)
        {
            _cardValidate = cardValidate;
        }
        #endregion
        #region post
        [HttpPost("ValidateCreditCard")]
        public IActionResult Post([FromBody] CreditCardInfo request)
        {
            if (DateTime.Now.Month > request.ExpMonth)
            {
                ModelState.AddModelError("ExpMonth", "Your card's expiration month is invalid");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var card = _cardValidate.ValidateCreditCardInfo(request);
            return Ok(card);
        }
        #endregion
    }
}
