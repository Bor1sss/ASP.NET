using Microsoft.AspNetCore.Http;

namespace RequestProcessingPipeline
{
    public class From10kk_100kMiddle
    {
        private readonly RequestDelegate _next;

        public From10kk_100kMiddle(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"]; // Получим число из контекста запроса
            try
            {
                string? result = string.Empty;
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);



                if (number < 10000)
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }
                else
                {
                    string[] TensThousands = { "twenty Thousand", "thirty Thousand", "forty Thousand", "fifty Thousand", "sixty Thousand", "seventy Thousand", "eighty Thousand", "ninety Thousand" };
                    int math = number / 10000  - 2;

                    if (number % 1000 == 0)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + TensThousands[math]);
                    }
                    else if (number/10000 > 19 && number > 9999)
                    {
                        if (number % 10 != 0)
                        {
                            await _next.Invoke(context);
                            result = context.Session.GetString("number");
                        }// Контекст запроса передаем следующему компоненту

                        context.Session.SetString("number", TensThousands[math]);

                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                        result = context.Session.GetString("number");


                        await context.Response.WriteAsync("Your number is " + TensThousands[math] + " " + result);
                    }
                }
            }
            catch (Exception)
            {
      
            }
        }
    }
}
