using Millenium_rekru.Exceptions;

namespace Millenium_rekru.Middlewares;

public class RequestCounterMiddleware(RequestDelegate next)
{
    private static int Counter = 0;
    private readonly Lock _counterLock = new();
    public async Task InvokeAsync(HttpContext context)
    {
        var error = false;
        lock (_counterLock)
        {
            Counter++;
            if (Counter == 10)
            {
                error = true;
                Counter = 0;
            }
        }

        if (!error)
        {
            await next(context);
        }
        
        throw new TenthRequestException();
    }

}