using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services.Contract;

namespace Talabat.APIS.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTimeInSeconds;

        public CachedAttribute(int ExpireTimeInSeconds)
        {
            _expireTimeInSeconds = ExpireTimeInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            //Explicitly

            var CacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();


            var CacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await   CacheService.GetCachedResponse(CacheKey);


            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = contentResult;
                return;
            }

            var ExcutedEndPointContext = await next.Invoke(); //Excute Endpoint

            if(ExcutedEndPointContext.Result is OkObjectResult result )
            {
                await CacheService.CacheResponseAsync(CacheKey, result.Value ,TimeSpan.FromSeconds(_expireTimeInSeconds));
            }

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();

            KeyBuilder.Append(request.Path);

            foreach (var (key,value) in request.Query.OrderBy(x => x.Key))
            {
                KeyBuilder.Append($"|{key}-{value}");
            }
            return KeyBuilder.ToString();

        }

    }
}
