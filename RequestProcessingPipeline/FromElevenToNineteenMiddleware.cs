namespace RequestProcessingPipeline
{
    public class FromElevenToNineteenMiddleware
    {
        private readonly RequestDelegate _next;

        public FromElevenToNineteenMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number % 100 < 11 || number % 100 > 19&&number%10!=0)
                {
                    await _next.Invoke(context);  //Контекст запроса передаем следующему компоненту
                }
                else
                {
                    string[] Numbers = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                    if (number<99)
                    {
                        
                        await context.Response.WriteAsync("Your number is " + Numbers[number - 11]);
                    }
                    else
                    {
                        context.Session.SetString("number", Numbers[number % 10 - 1]);
                    }
                }
            }
            catch (Exception)
            {
          
            }
        }
    }
}
