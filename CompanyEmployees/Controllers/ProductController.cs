using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = Permissions.Products.View)]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Products.Create)]
    public IActionResult Create()
    {
        return Ok();
    }
}