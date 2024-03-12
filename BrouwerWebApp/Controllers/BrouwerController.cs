using BrouwerWebApp.Models;
using BrouwerWebApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrouwerWebApp.Controllers
{

    //Met dit project combineren we normale controllers MVC met REST controllers
    //Deze controller is een REST controller owv webAPI

    [Route("brouwerss")]
    [ApiController]
    public class BrouwerController(IBrouwerRepository repository) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id) =>
            await repository.FindByIdAsync(id) is Brouwer brouwer ? base.Ok(brouwer) :
            base.NotFound();
    }
}
