namespace RequestProcessingPipeline
{
    public class From500_1000Middle
    {
        private readonly RequestDelegate _next;

        public From500_1000Middle(RequestDelegate next)
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
                if (number < 1000)
                {
                    await _next.Invoke(context);
                }
                else if(number > 1000)
                {

                    await context.Response.WriteAsync("Number greater than 1000 but less than 2000");
                }
                else if (number == 1000)
                {
          
                    await context.Response.WriteAsync("Your number is 1000");
                }
                else
                {
                    
                }              
            }
            catch (Exception)
            {
                // Выдаем окончательный ответ клиенту
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
