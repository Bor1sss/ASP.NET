using Microsoft.AspNetCore.Http;

namespace RequestProcessingPipeline
{
    public class From1000_10kMiddle
    {
        private readonly RequestDelegate _next;

        public From1000_10kMiddle(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number < 1000)
                {
                    await _next.Invoke(context); // Передать управление следующему обработчику в конвейере запросов
                    return; // Прерываем выполнение кода в текущем middleware
                }
                else
                {
                    string[] Thousands = { "One thousand", "Two thousand", "Three thousand", "Four thousand", "Five thousand", "Six thousand", "Seven thousand", "Eight thousand", "Nine thousand" };
                    int math = number % 100 / 10 - 1;
                    if (number % 1000 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + Thousands[number / 1000 - 1]);
                    }



                    else
                    {
                       
                     

                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number"); 
                        if (number > 9999)
                        {
                            if (number % 1000 == 0)
                            {
                                context.Session.SetString("number", Thousands[math] + result);
                            }
                            else
                            {
                                context.Session.SetString("number", result);
                            }
                        }                 
                        else
                        {
                           
                            await context.Response.WriteAsync("Your number is " + Thousands[number / 1000 - 1] + " " + result);
                        }
                    }
                }
            }
            catch (Exception)
            {
      
            }
        }
    }
}
