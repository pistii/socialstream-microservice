using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace GrpcServices
{
    public static class GrpcControllerHelper
    {
        public static async Task<TResponse> HandleControllerResponse<TResponse>(Task<IActionResult> controllerTask, Func<object, TResponse> mapFunction)
        {
            var actionResult = await controllerTask;

            if (actionResult is OkObjectResult okResult && okResult.Value is not null)
            {
                return mapFunction(okResult.Value);
            }
            else if (actionResult is NoContentResult noContentResult)
            {
                return mapFunction("No content");
            }
            else
            {
                throw new RpcException(new Status(StatusCode.Internal, "Unexpected controller response or error."));
            }
        }
    }

}
