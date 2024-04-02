namespace RequestProcessingPipeline
{
    public class From3125_6250Middle
    {
        private readonly RequestDelegate _next;

        public From3125_6250Middle(RequestDelegate next)
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
                if (number < 6250)
                {
                    await _next.Invoke(context);
                }
                else if(number > 6250)
                {

                    await context.Response.WriteAsync("Number greater than 6250 but less than 12500");
                }
                else if (number == 6250)
                {
          
                    await context.Response.WriteAsync("Your number is 6250");
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
