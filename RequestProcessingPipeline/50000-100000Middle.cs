namespace RequestProcessingPipeline
{
    public class From50000_100000Middle
    {
        private readonly RequestDelegate _next;

        public From50000_100000Middle(RequestDelegate next)
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
                if (number < 50000)
                {
                    await _next.Invoke(context);
                }
                else if(number > 50000)
                {

                    await context.Response.WriteAsync("Number greater than 50000 less than 100000");
                }
                else if (number == 50000)
                {
          
                    await context.Response.WriteAsync("Your number is 50000");
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
