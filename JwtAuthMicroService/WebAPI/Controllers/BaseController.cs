using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public abstract class BaseController : ControllerBase { }