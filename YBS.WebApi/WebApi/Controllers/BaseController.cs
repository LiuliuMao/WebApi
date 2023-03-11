using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("[controller]")]
    [Authorize]
    public class BaseController: ControllerBase
    {
    }
}
