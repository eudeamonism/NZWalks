using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddlware
    {
        private readonly ILogger<ExceptionHandlerMiddlware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddlware(ILogger<ExceptionHandlerMiddlware> logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log this Exception
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                //Return A Custom Error Response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                //Custom Error Model
                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are looking into resolvin this."
                };


                await httpContext.Response.WriteAsJsonAsync( error );
            }
        }


    }
}
