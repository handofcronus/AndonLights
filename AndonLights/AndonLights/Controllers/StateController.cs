using AndonLights.DTOs;
using AndonLights.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AndonLights.Controllers;


[ApiController]
[Route("/api/v1/[controller]")]
public class StateController : ControllerBase
{
    private IStateService _stateService;
    private ILogger _logger;
    public StateController(ILogger logger,IStateService service)    
    { 
        _stateService = service;
        _logger = logger;
    }


    [HttpGet("/daily")]
    public ActionResult<StatsResponseDTO> GetDailyStats([FromBody] StatsQuestionDTO statsQuestion)
    {
        try
        {
            var res = _stateService.GetDailyStats(statsQuestion);
            return res == null ? NotFound() : Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(GetDailyStats), e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("/monthly")]
    public ActionResult<StatsResponseDTO> GetMonthlyStats([FromBody] StatsQuestionDTO statsQuestion)
    {
        try
        {
            var res = _stateService.GetMonthlyStats(statsQuestion);
            return res == null ? NotFound() : Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(GetDailyStats), e);
            return BadRequest(e.Message);
        }
    }

}
