using Microsoft.AspNetCore.Http;

namespace RequestProcessingPipeline
{
    public class From10k_10kkMiddle
    {
        private readonly RequestDelegate _next;

        public From10k_10kkMiddle(RequestDelegate next)
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
                if (number < 10000)
                {
                    await _next.Invoke(context); // Передать управление следующему обработчику в конвейере запросов
                    return; // Прерываем выполнение кода в текущем middleware
                }
                else
                {
                    string[] Tenthousands = { "eleven Thousands", "twelve Thousands", "thirteen Thousands", "fourteen Thousands", "fifteen Thousands", "sixteen Thousands", "seventeen Thousands", "eighteen Thousands", "nineteen Thousands" };
                    int math = number % 10000 / 1000 - 1;

                    if (number % 10000 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + " ten thousand");
                    }
                    else
                    {



                            if ((number / 1000 < 11 || number / 1000 > 19)&& number%1000!=0)
                            {
                                await _next.Invoke(context);  //Контекст запроса передаем следующему компоненту
                            }
                            else
                            {
                                await _next.Invoke(context);
                                string? result = string.Empty;
                                result = context.Session.GetString("number");

                                if (number < 99999)
                                {
                                    
                                    await context.Response.WriteAsync("Your number is " + Tenthousands[math]+result);
                                }
                                else
                                {
                                    context.Session.SetString("number", Tenthousands[math]+result);
                                }

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
