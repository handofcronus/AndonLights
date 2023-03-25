using AndonLights.DAL.Interfaces;
using AndonLights.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AndonLights.Controllers;


[ApiController]
[Route("[controller]")]
public class StateController : ControllerBase
{
    private IStateService _stateService;
    StateController(IStateService service)    
    { 
        _stateService = service;
    }


    [HttpGet("/daily")]
    public ActionResult<StatsResponseDTO> GetDailyStats([FromBody] StatsQuestionDTO statsQuestion)
    {
        return Ok();
    }

    [HttpGet("/monthly")]
    public ActionResult<StatsResponseDTO> GetMonthlyStats([FromBody] StatsQuestionDTO statsQuestion)
    {
        return Ok();
    }

}
