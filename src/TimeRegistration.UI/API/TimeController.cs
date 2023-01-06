using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRegistration.BusinessLogic.TimeRegistration;

namespace TimeRegistration.UI.API;

[ApiController]
[Route("api/[controller]")]
public class TimeController : ControllerBase
{
    private readonly TimeRegistrationService _timeRegistrationService;

    public TimeController(TimeRegistrationService timeRegistrationService)
    {
        _timeRegistrationService = timeRegistrationService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTask(CreateModelDto model)
    {
        /* API Create task should:
         - make a post request (in the back-end call TimeRegistration.TimeRegistrationService.Create)
            *this depends on DEPENDENCY INJECTION! => Inject timeregistration service (ctor) and instantiate a private variable!*
         - check for errors..
         - if succesful return object in header
        */
        var internalModel = new CreateModel()
        {
            Description = model.Title, Title = model.Title, StartTime = model.StartTime
        };

        var results = await _timeRegistrationService.Create(internalModel);

        return CreatedAtAction("GetById", TimeEntryDto.Create(results));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entry = await _timeRegistrationService.GetById(id);
        return entry == null ? NotFound() : Ok(TimeEntryDto.Create(entry));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById([FromQuery] int id)
    {
        try
        {
            await _timeRegistrationService.Delete(id);
            return Ok();
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }

    }


    [HttpGet]
    public async Task<IActionResult> Get(int pageSize, int pageNumber, string? orderBy)
    {

        var result = await _timeRegistrationService.Get(pageSize, pageNumber, orderBy);
        var records = result.Records.Select(x => TimeEntryDto.Create(x)).ToArray();

        return Ok(new
        {
            // todo write class instead of anonymous object 
            records, result.TotalCount
        });

    }

}
