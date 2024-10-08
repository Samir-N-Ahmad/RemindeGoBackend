


using System.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RemindeGo.API.Extentions;
using RemindeGo.Common.Contracts;
using RemindeGo.Service;

namespace RemindeGo.Controller;

[Route("Reminders")]
public class RemindersController(IReminderService reminderService) : ControllerBase
{

    private readonly IReminderService _reminderService = reminderService;

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

    [HttpPost("Create")]
    public async Task<IResult> Create([FromBody] CreateReminderRequest request)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState.ValidationState);
        }
        try
        {
            var reminder = await _reminderService.CreateReminder(request);
            return reminder.HandleErrorOr<CreateReminderResult>();
        }
        catch (Exception)
        {
            return Results.BadRequest("Unknown Error happened");
        }
    }
}


