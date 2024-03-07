using BrouwerService.Models;
using BrouwerService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrouwerService.Controllers
{
    [Route("brouwers")]
    [ApiController]
    public class BrouwerController(IBrouwerRepository repository) : ControllerBase
    {

        /*------------------------GET--------------------------------------*/

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


        /*-----------------------------DELETE--------------------------------------*/

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var brouwer = repository.FindById(id);
            if(brouwer == null)
                return base.NotFound();

            repository.Delete(brouwer);
            return base.Ok();
        }

        /*------------------------------POST------------------------------------------*/

        [HttpPost]
        public IActionResult Post(Brouwer brouwer)
        {
            repository.Insert(brouwer);
            return base.CreatedAtAction(nameof(FindById), new { id = brouwer.Id }, null);
        }


        /*----------------------------PUT---------------------------------------------*/

        [HttpPut("{id}")]
        public IActionResult Put(int id, Brouwer brouwer)
        {
            try
            {
                brouwer.Id = id;
                repository.Update(brouwer);
                return base.Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return base.NotFound();
            }
            catch
            {
                return base.Problem();
            }
        }
    }
}
