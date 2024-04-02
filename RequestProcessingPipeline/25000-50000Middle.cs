namespace RequestProcessingPipeline
{
    public class From25000_50000Middle
    {
        private readonly RequestDelegate _next;

        public From25000_50000Middle(RequestDelegate next)
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
                if (number < 25000)
                {
                    await _next.Invoke(context);
                }
                else if(number > 25000)
                {

                    await context.Response.WriteAsync("Number greater than 25000 but less than 50000");
                }
                else if (number == 25000)
                {
          
                    await context.Response.WriteAsync("Your number is 25000");
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
