namespace Thimble.Product.Middleware.ErrorHandling.ErrorResponses
{
    public static class StaticErrorResponses
    {
        public static readonly BaseErrorResponse ProductNotFound = new BaseErrorResponse
        {
            StatusCode = 404,
            Message = "There is no product with the provided ProductId"
        };
        
        public static readonly BaseErrorResponse AuthHeaderRequired = new BaseErrorResponse
        {
            StatusCode = 401,
            Message = "The Authorization header is required"
        };
        
        public static readonly BaseErrorResponse InvalidAuthKey = new BaseErrorResponse
        {
            StatusCode = 401,
            Message = "The Authorization key provided is invalid"
        };
        
    }
}