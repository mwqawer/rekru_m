using Millenium_rekru.Exceptions;

namespace Millenium_rekru.Middlewares;

public class RequestCounterMiddleware(RequestDelegate next)
{
    private static int Counter = 0;

    public async Task InvokeAsync(HttpContext context)
    {
        Counter++;
        if (Counter != 10)
        {
            await next(context);
        }
        
        Counter = 0;
        throw new TenthRequestException();
    }

}