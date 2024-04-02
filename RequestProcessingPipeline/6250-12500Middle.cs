namespace RequestProcessingPipeline
{
    public class From6250_12500Middle
    {
        private readonly RequestDelegate _next;

        public From6250_12500Middle(RequestDelegate next)
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
                if (number < 12500)
                {
                    await _next.Invoke(context);
                }
                else if(number > 12500)
                {

                    await context.Response.WriteAsync("Number greater than 12500 but less than 25000");
                }
                else if (number == 12500)
                {
          
                    await context.Response.WriteAsync("Your number is 12500");
                }
                else
                {
                    
                }              
            }
            catch (Exception)
            {
              
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
