using AndonLights.Controllers.Attributes;
using AndonLights.DTOs;
using AndonLights.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AndonLights.Controllers;

[TypeFilter(typeof(ApiKeyAttribute))]
[ApiController]
[Route("/api/v2/[controller]")]
public class StateControllerVersion2 : ControllerBase
{
    private readonly IStateService _stateService;
    private readonly ILogger _logger;
    private readonly IAndonLightService _lightService;
    public StateControllerVersion2(ILogger<StateController> logger,IStateService service,IAndonLightService lightService)    
    { 
        _stateService = service;
        _logger = logger;
        _lightService = lightService;
    }


    [HttpGet("statdaily")]
    public ActionResult<StatsResponseDTO> GetDailyStats([FromQuery] StatsQuestionVersion2DTO statsQuestion)
    {
        try
        {
            var q = new StatsQuestionDTO
            {
                Date = new DateTime(statsQuestion.Year, statsQuestion.Month, statsQuestion.Day),
            };
            var res = _stateService.GetDailyStats(q);
            return res == null ? NotFound() : Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(GetDailyStats), e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("statmonthly")]
    public ActionResult<StatsResponseDTO> GetMonthlyStats([FromQuery] StatsQuestionVersion2DTO statsQuestion)
    {
        try
        {
            var q = new StatsQuestionDTO
            {
                Date = new DateTime(statsQuestion.Year, statsQuestion.Month, statsQuestion.Day),
            };
            var res = _stateService.GetMonthlyStats(q);
            return res == null ? NotFound() : Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(GetDailyStats), e);
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    public ActionResult<AndonStateDTO> SwitchState([FromBody] AndonStateDTO dto)
    {
        try
        {
            return _lightService.SwitchState(dto);

        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(SwitchState), e);
            return BadRequest(e.Message);
        }
    }
    [HttpGet]
    public ActionResult<List<AndonStateDTO>> GetAllState()
    {
        try
        {
            return  Ok(_lightService.GetStates());
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(GetAllState), e);
            return BadRequest(e.Message);
        }
    }


    [HttpGet("{id}")]
    public ActionResult<AndonStateDTO> GetState(int id)
    {
        try
        {
           return Ok(_lightService.GetState(id));
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(GetState), e);
            return BadRequest(e.Message);
        }
    }

}
