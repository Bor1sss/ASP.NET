using Microsoft.AspNetCore.Http;
using System;

namespace RequestProcessingPipeline
{
    public class From100_1000Middle
    {
        private readonly RequestDelegate _next;

        public From100_1000Middle(RequestDelegate next)
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
                if (number < 100)
                {
                    await _next.Invoke(context); // Передать управление следующему обработчику в конвейере запросов
                }
                else
                {
                    string[] Hundreds = { "One hundred", "Two hundred", "Three hundred", "Four hundred", "Five hundred", "Six hundred", "Seven hundred", "Eight hundred", "Nine hundred" };
                    int math = number % 1000 / 100 - 1;
                    string? result = string.Empty;
                    if (number % 100 == 0 && number<1000)
                    {
                        await context.Response.WriteAsync("Your number is " + Hundreds[number / 100 - 1]);
                    }
                    else if(number % 100 == 0 && number > 1000)
                    {
                        context.Session.SetString("number", Hundreds[math]);
                    }
                    else
                    {
                        await _next.Invoke(context);
                       
                      
                        result = context.Session.GetString("number");
                    
                        
                        if (number > 999)
                        {

                            if (math <0)
                            {

                                context.Session.SetString("number", result);
                          
                            }
                            else
                            {

                                context.Session.SetString("number", Hundreds[math] + result);
                           
                            }
                        }
                        else
                        {
                           
                            await context.Response.WriteAsync("Your number is " + Hundreds[number / 100 - 1] + " " + result);
                        
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
