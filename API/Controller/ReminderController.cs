


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReminderApp.Contracts;

namespace ReminderApp.Controller;

[Route("Reminders")]
public class RemindersController : ControllerBase
{



    [Route("GetAll")]
    public async Task<IResult> GetAll()
    {

        await Task.Delay(10);
        return Results.Ok("sdf");
    }

    [Route("Get")]
    public async Task<IResult> GetAll([FromBody] GetReminderRequest request)
    {

        await Task.Delay(10);
        return Results.Ok("sdf");
    }
}


