using BrouwerService.Models;
using BrouwerService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrouwerService.Controllers
{
    [Route("brouwers")]
    [ApiController]
    public class BrouwerController(IBrouwerRepository repository) : ControllerBase
    {
        [HttpGet]
        public IActionResult FindAll() => base.Ok(repository.FindAll());

        [HttpGet("{id}")]
        //public IActionResult FindById(int id)
        //{
        //    var brouwer = repository.FindById(id);
        //    return brouwer == null? base.NotFound() : base.Ok(brouwer);
        //}
        
        //verkorte versie
        public IActionResult FindById(int id) => 
            repository.FindById(id) is Brouwer brouwer ? base.Ok(brouwer) : base.NotFound();

        // URl --> brouwers/naam?begin=a --> geeft alle met a in naam
        [HttpGet("naam")]
        public IActionResult FindByBeginNaam(string begin) =>
            base.Ok(repository.FindByBeginNaam(begin));
    }
}
