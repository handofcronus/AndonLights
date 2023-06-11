using AndonLights.DTOs;
using AndonLights.Model;
using AndonLights.Services;
using AndonLights.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AndonLights.Controllers;


[ApiController]
[Route("/api/v2/[controller]")]
public class AndonLightControllerVersion2 : ControllerBase
{
    private readonly IAndonLightService _lightService;
    private readonly ILogger _logger;
    private readonly IClientService _clientService;
    public AndonLightControllerVersion2(ILogger<AndonLightController> logger, IAndonLightService andonLightService,IClientService clientService)
    {
        _lightService = andonLightService;
        _logger = logger;
        _clientService = clientService;
    }


    /// <summary>
    /// Retrieves all the lights in full detail.
    /// </summary>
    /// <returns>Returns the lights</returns>
    /// <response code="200">Ok</response>
    /// <response code="404">Lights not found</response>

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<AndonLightDTO>> GetLights()
    {
        try
        {
            var lights = _lightService.GetLights();
            return lights == null ? NotFound() : Ok(lights);
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at "+nameof(GetLights), e);
            return BadRequest(e.Message);
        }

    }

    /// <summary>
    /// Retrieves a light in full detail.
    /// </summary>
    /// <param name="id">The light's id</param>
    /// <returns>Returns the light</returns>
    /// <response code="200">Ok</response>
    /// <response code="404"> Light not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<AndonLightDTO> GetLight(int id)
    {
        try
        {
            var light = _lightService.GetLight(id);
            return light == null ? NotFound() : Ok(light);
        }


        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(GetLight), e);
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Creates a light with the given name.
    /// </summary>
    /// <param name="name">The light's name</param>
    /// <returns>Returns the result of the request</returns>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad request</response>
    [HttpPost("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<AndonLightDTO> CreateLight(string name)
    {
        try
        {
            var created = _lightService.CreateLight(name);
            return CreatedAtAction(nameof(GetLight), new { id = created.ID }, created);
        }

        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(CreateLight), e);
            return BadRequest(e.Message);
        }

    }
    /// <summary>
    /// Deletes a light with the id.
    /// </summary>
    /// <param name="id">The light's id</param>
    /// <returns>Returns the result of the request</returns>
    /// <response code="200">Ok</response>
    /// <response code="404">Light not found with this id</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteLight(int id)
    {
        try
        {
            var res = _lightService.DeleteLight(id);
            return res ? Ok() : NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(DeleteLight), e);
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Update a lights name.
    /// </summary>
    /// <param name="andonLight">Dto with the light's id and name</param>
    /// <returns>Returns the changed light</returns>
    /// <response code="200">Ok</response>
    /// <response code="404">Light not found with this id</response>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AndonLightDTO> UpdateLight([FromBody] UpdateLightDTO andonLight)
    {
        try
        {
            var res = _lightService.UpdateLight(andonLight);
            return res == null ? NotFound() : Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(UpdateLight), e);
            return BadRequest(e.Message);
        }

    }
    /// <summary>
    /// Request an ApiKey for a new client.
    /// </summary>
    /// <param name="name">Client's identifier</param>
    /// <returns>Returns an ApiKey for the client</returns>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>
    [HttpPost("/client/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> RequestApiKey(string name)
    {
        try
        {
            var res = _clientService.CreateClient(name);
            return Ok(res.ApiKey);
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(RequestApiKey), e);
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Request a new apiKey for an existing client.
    /// </summary>
    /// <param name="name">Client's identifier</param>
    /// <returns>Returns a new ApiKey for the client</returns>
    /// <response code="200">Ok</response>
    /// <response code="400">BadRequest</response>

    [HttpPost("/client/newKey/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> RequestNewApiKey(string name)
    {
        try
        {
            var res = _clientService.RequestNewKey(name);
            return Ok(res.NewApiKey);
        }
        catch (Exception e)
        {
            _logger.LogError("Error happened at " + nameof(RequestApiKey), e);
            return BadRequest(e.Message);
        }
    }
}
