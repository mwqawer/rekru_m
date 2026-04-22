using Microsoft.AspNetCore.Mvc;
using Millenium_rekru.Exceptions;
using Millenium_rekru.Requests;
using Millenium_rekru.Services;

namespace Millenium_rekru.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController(IDataProcessingService processingService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]ProcessDataRequest request)
    {
        var guid = Guid.NewGuid().ToString();
        processingService.ProcessDataAsync(guid, request.Data);

        return Accepted(guid+"/status");
    }
    
    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        try
        {
            return Ok(processingService.GetData(key));
        }
        catch (DataNotFoundException ex)
        {
            return NotFound();
        }
        catch (ProcessingPendingException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{key}/status")]
    public async Task<IActionResult> Status(string key)
    {
        try
        {
            return Ok(processingService.GetStatus(key));
        }  catch (DataNotFoundException ex)
        {
            return NotFound();
        }
    }
    
}