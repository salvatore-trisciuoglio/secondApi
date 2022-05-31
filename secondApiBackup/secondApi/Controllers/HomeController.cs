using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using secondApi.Models;

namespace secondApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class HomeController : Controller
{
    [HttpGet("Get String")]

    public string GetString() {
        return "Demo Application";

    }
}

