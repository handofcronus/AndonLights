using AndonLights.DAL.Interfaces;
using AndonLights.DTOs;
using AndonLights.Services;
using Microsoft.AspNetCore.Mvc;

namespace AndonLights.Controllers;


[ApiController]
[Route("[controller]")]
public class AndonLightController : ControllerBase
{
    private IAndonLightService _lightService;
    public AndonLightController(IAndonLightService andonLightService)
    {
        _lightService = andonLightService;

    }



    [HttpGet]
    public ActionResult<IEnumerable<AndonLightDTO>> GetLights()
    {
        var lights = _lightService.GetLights();
        return lights == null ? NotFound() : Ok(lights);
    }


    [HttpGet("{id}")]
    public ActionResult<AndonLightDTO> GetLight(int id)
    {
        var light = _lightService.GetLight(id);
        return light == null ? NotFound() : Ok(light);
    }


    [HttpPost("{name}")]
    public ActionResult<AndonLightDTO> CreateLight(string name)
    {

        var created = _lightService.CreateLight(name);
        return CreatedAtAction(nameof(GetLight), new { id = created.ID }, created);


    }

    [HttpDelete]
    public ActionResult DeleteLight(int id) 
    {
        var res = _lightService.DeleteLight(id);
        return res==true ? NotFound() : Ok();
    }


    [HttpPatch]
    public ActionResult<AndonLightDTO> UpdateLight([FromBody] AndonLightDTO andonLight)
    {
        var res = _lightService.UpdateLight(andonLight);
        return res == null ? NotFound() : Ok(res); 
    }







}
