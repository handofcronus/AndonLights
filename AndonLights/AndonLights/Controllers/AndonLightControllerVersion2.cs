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


    

    [HttpGet]
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


    [HttpGet("{id}")]
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


    [HttpPost("{name}")]
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

    [HttpDelete]
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


    [HttpPatch]
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

    [HttpPost("/client/{name}")]
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
}
