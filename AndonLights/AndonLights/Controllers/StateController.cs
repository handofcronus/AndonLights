using AndonLights.DTOs;
using AndonLights.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AndonLights.Controllers;


[ApiController]
[Route("/api/v1/[controller]")]
public class StateController : ControllerBase
{
    private readonly IStateService _stateService;
    private readonly ILogger _logger;
    private readonly IAndonLightService _lightService;
    public StateController(ILogger<StateController> logger,IStateService service,IAndonLightService lightService)    
    { 
        _stateService = service;
        _logger = logger;
        _lightService = lightService;
    }

    /// <summary>
    /// Gets the given days statistics about the light.
    /// </summary>
    /// <param name="statsQuestion">The dto containing the lights id and the date associated with statistics</param>
    /// <returns>Returns the statistics about the light on the given day</returns>
    /// <response code="200">Ok</response>
    /// <response code="404">Light not found with this id</response>
    /// <response code="400">Bad request</response>
    [HttpGet("daily")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// <summary>
    /// Gets the given months statistics about the light.
    /// </summary>
    /// <param name="statsQuestion">The dto containing the lights id and the date associated with statistics</param>
    /// <returns>Returns the statistics about the light in the given month</returns>
    /// <response code="200">Ok</response>
    /// <response code="404">Light not found with this id</response>
    /// <response code="400">Bad request</response>
    [HttpGet("monthly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Switch a lights state
    /// </summary>
    /// <param name="dto">The dto containing the lights id and the new state with the optional error message</param>
    /// <returns>Returns the light in the new state</returns>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    /// <summary>
    /// Retrieves all the states of the lights
    /// </summary>
    /// <returns>Returns the states of the lights</returns>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


    /// <summary>
    /// Retrieves the state of the light with the id
    /// </summary>
    /// <param name="id">The light's id</param>
    /// <returns>Returns the lights state</returns>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
