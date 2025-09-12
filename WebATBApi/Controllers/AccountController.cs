using Core.Interfaces;
using Core.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace AtbWebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterModel model)
    {
        var result = await accountService.RegisterAsync(model);
        if (result.Success)
            return Ok(new { Token = result.Token });

        return BadRequest(new
        {
            status = 400,
            isValid = false,
            errors = result.ErrorMessage
        });
    }
}
