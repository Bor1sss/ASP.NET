﻿using System;

namespace RequestProcessingPipeline
{
    public class FromTwentyToHundredMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTwentyToHundredMiddleware(RequestDelegate next)
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
                if (number < 20||number%100<20)
                {
                    await _next.Invoke(context); //Контекст запроса передаем следующему компоненту
                }
                else
                {
                    string? result = string.Empty;
                    int math = number % 100 / 10 - 2;
                    string[] Tens = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                    if (number % 10 == 0&&number<99)
                    {
                        // Выдаем окончательный ответ клиенту
                        await context.Response.WriteAsync("Your number is " + Tens[math]); 
                    }
                    else if(number>19 && number > 99)
                    {
                        if (number % 10 != 0)
                        {
                            await _next.Invoke(context);
                            result = context.Session.GetString("number");
                        }// Контекст запроса передаем следующему компоненту
                       
                        context.Session.SetString("number", Tens[math]+result);

                    }
                    else
                    {
                        await _next.Invoke(context); // Контекст запроса передаем следующему компоненту
                        result = context.Session.GetString("number");
                      
                       
                        await context.Response.WriteAsync("Your number is " + Tens[math] + " " + result);
                    }
                }              
            }
            catch (Exception)
            {
             
            }
        }
    }
}
