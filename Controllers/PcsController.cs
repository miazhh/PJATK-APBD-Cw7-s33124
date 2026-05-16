using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw7_s33124.DTOs;
using PJATK_APBD_Cw7_s33124.Exceptions;
using PJATK_APBD_Cw7_s33124.Services;

namespace PJATK_APBD_Cw7_s33124.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PcsController(IPcsService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPcs()
    {
        var pcs = await service.GetAllPcsAsync();
        return Ok(pcs);
    }

    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetPcComponents(int id)
    {
        try
        {
            var components = await service.GetPcComponentsAsync(id);
            return Ok(components);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPc([FromBody] CreatePcDto request)
    {
        var pc = await service.CreatePcAsync(request);
        return Created($"api/pcs/{pc.Id}", pc);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePc(int id, [FromBody] UpdatePcDto request)
    {
        try
        {
            await service.UpdatePcAsync(id, request);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePc(int id)
    {
        try
        {
            await service.DeletePcAsync(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}