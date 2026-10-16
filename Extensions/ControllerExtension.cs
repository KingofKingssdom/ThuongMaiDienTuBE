using Ecommerce.Common.Enum;
using Ecommerce.Common.Responses;
using Ecommerce.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Extensions
{
    public static class ControllerExtension
    {
        public static IActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess)
            {
                return controller.Ok(FactoryResponse.Success(result.Value!, result.Message));
            }

            return result.ErrorType switch
            {
                ErrorType.Validation => controller.BadRequest(FactoryResponse.Validation(new[] { result.Error! })),

                ErrorType.NotFound => controller.NotFound(FactoryResponse.Error(result.Error!)),

                ErrorType.Unauthorized => controller.Unauthorized(FactoryResponse.Error(result.Error!)),

                ErrorType.Forbidden => controller.StatusCode(StatusCodes.Status403Forbidden, FactoryResponse.Error(result.Error!)),

                _ => controller.BadRequest(FactoryResponse.Error(result.Error!))
            };
        }

        public static IActionResult ToActionResult(this ControllerBase controller, Result result)
        {
            if (result.IsSuccess)
            {
                return controller.Ok(new
                {
                    success = true,
                    message = result.Message
                });
            }

            return result.ErrorType switch
            {
                ErrorType.Validation =>
                    controller.BadRequest(new
                    {
                        success = false,
                        message = result.Error
                    }),

                ErrorType.NotFound =>
                    controller.NotFound(new
                    {
                        success = false,
                        message = result.Error
                    }),

                ErrorType.Conflict =>
                    controller.Conflict(new
                    {
                        success = false,
                        message = result.Error
                    }),

                ErrorType.Unauthorized =>
                    controller.Unauthorized(new
                    {
                        success = false,
                        message = result.Error
                    }),

                _ =>
                    controller.StatusCode(500, new
                    {
                        success = false,
                        message = result.Error
                    })
            };
        }
    }
}
