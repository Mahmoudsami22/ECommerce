using ECommerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.API.Attributes
{
    public class RedisCacheAttribute:ActionFilterAttribute
    {
        private readonly int _durationInSec;

        public RedisCacheAttribute(int DurationInSec)
        {
            _durationInSec = DurationInSec;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            //Check if Cached Data Exsists
            var Cached = await cacheService.GetAsync(cacheKey);
            //If Exsists ,Return Cached Data and Skip Exceution of EndPoint
            if (!string.IsNullOrEmpty(Cached))
            {
                context.Result = new ContentResult()
                {
                    Content = Cached,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }
            //If Not Exsist , Execute EndPoint ,and Store The Result in Cache if 200 Ok Response
            var Executed = await next.Invoke();
            if (Executed.Result is OkObjectResult { Value: not null } ok)
                await cacheService.SetAsync(cacheKey, ok.Value, TimeSpan.FromSeconds(_durationInSec));
            return;
        }


        private static string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path).Append("?");


            foreach (var (k, v) in request.Query.OrderBy(Q => Q.Key))
            {
                key.Append(k).Append("=").Append(v).Append("&");
            }
            return key.ToString();
        }


    }
}
