using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PreAnnouncement.Lambda.Common
{
    internal static class ResponseHelper
    {
        public static APIGatewayProxyResponse BadRequest() => new APIGatewayProxyResponse() { StatusCode = (int)HttpStatusCode.BadRequest };
        public static APIGatewayProxyResponse NotFound() => new APIGatewayProxyResponse() { StatusCode = (int)HttpStatusCode.NotFound };
        public static APIGatewayProxyResponse ServerError(Exception exp) => new APIGatewayProxyResponse()
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Body = JsonConvert.SerializeObject(new { Exception = exp.GetType().ToString() }),
        };
        public static APIGatewayProxyResponse OkResponse(object response) => new APIGatewayProxyResponse()
        {
            Body = JsonConvert.SerializeObject(response),
            StatusCode = (int)HttpStatusCode.OK,
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
        public static APIGatewayProxyResponse OkResponse() => new APIGatewayProxyResponse()
        {
            StatusCode = (int)HttpStatusCode.OK
        };
    }
}
