


using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ReminderApp.API.Extentions;
public static class ResultExtenstions
{


    public static IResult HandleErrorOr<T>(this ErrorOr.ErrorOr<T> result)
    {

        if (result.IsError)
        {
            var error = result.Errors.First();
            ///TODO: Hanlde multiple errors
            switch (error.Type)
            {
                case ErrorOr.ErrorType.Unauthorized:
                    {
                        return Results.Unauthorized();
                    }
                case ErrorOr.ErrorType.Conflict:
                    {
                        return Results.Conflict(error.Description);
                    }
                case ErrorOr.ErrorType.NotFound:
                    {
                        return Results.NotFound(error.Description);
                    }
                case ErrorOr.ErrorType.Forbidden:
                    {
                        return Results.Forbid();
                    }
                case ErrorOr.ErrorType.Validation:
                    {
                        return Results.BadRequest(error.Description);
                    }
                default:
                    {
                        return Results.BadRequest(error.Description);
                    }
            }
        }
        else
        {

            return Results.Ok(result.Value);
        }
    }

    public static IResult HandleValidationError(this ModelStateDictionary state)
    {
        return Results.BadRequest(state.Values.SelectMany(value => value.Errors.Select(error => error.ErrorMessage)).Aggregate((acc, value) => acc + $", \n ${value}"));
    }
}