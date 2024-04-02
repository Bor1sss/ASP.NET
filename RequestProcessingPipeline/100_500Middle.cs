namespace RequestProcessingPipeline
{
    public class From100_500Middle
    {
        private readonly RequestDelegate _next;

        public From100_500Middle(RequestDelegate next)
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
                if (number < 500)
                {
                    await _next.Invoke(context);
                }
                else if(number > 500)
                {

                    await context.Response.WriteAsync("Number greater than 500 but less than 1000");

                }
                else if (number == 500)
                {
          
                    await context.Response.WriteAsync("Your number is 500");
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
