using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers.Generic;

[ApiController]
public class ApiController : ControllerBase
{
    protected async Task<IActionResult> BuildErrorResponse(HttpResponseMessage response)
    {
        var errorContent = await response.Content.ReadAsStringAsync();
        return StatusCode((int)response.StatusCode, errorContent);
    }
}