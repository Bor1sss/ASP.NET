namespace RequestProcessingPipeline
{
    public class From2000_3125Middle
    {
        private readonly RequestDelegate _next;

        public From2000_3125Middle(RequestDelegate next)
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
                if (number < 3125)
                {
                    await _next.Invoke(context);
                }
                else if(number > 3125)
                {

                    await context.Response.WriteAsync("Number greater than 3125 but less than 6250");
                }
                else if (number == 3125)
                {
          
                    await context.Response.WriteAsync("Your number is 3125");
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
