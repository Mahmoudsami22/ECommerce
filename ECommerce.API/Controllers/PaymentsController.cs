using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Baskets;
using ECommerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentServices paymentServices;

        public PaymentsController(IPaymentServices paymentServices)
        {
            this.paymentServices = paymentServices;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId, CancellationToken ct)
        {
            var Result = await paymentServices.CreateOrUpdatePaymentIntentAsync(basketId, ct);

            return ToActionResult(Result);
        }

    }
}
