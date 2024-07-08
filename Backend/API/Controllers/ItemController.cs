using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]/[action]")]
public class ItemController(IItemService itemService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string s)
    {
        return Ok(await itemService.GetFromSearch(s));
    }
}