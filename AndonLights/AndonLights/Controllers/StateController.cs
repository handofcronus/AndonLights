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
    private IAndonLightService _lightService;
    public StateController(ILogger<StateController> logger,IStateService service,IAndonLightService lightService)    
    { 
        _stateService = service;
        _logger = logger;
        _lightService = lightService;
    }


    [HttpGet("/daily")]
    public ActionResult<StatsResponseDTO> GetDailyStats([FromQuery] StatsQuestionDTO statsQuestion)
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
    public ActionResult<StatsResponseDTO> GetMonthlyStats([FromQuery] StatsQuestionDTO statsQuestion)
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
