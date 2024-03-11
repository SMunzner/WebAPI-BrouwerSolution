using BrouwerService.Models;
using BrouwerService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BrouwerService.Controllers
{
    [Route("brouwers")]
    [ApiController]
    public class BrouwerController(IBrouwerRepository repository) : ControllerBase
    {

        /*------------------------GET--------------------------------------*/

        [HttpGet]
        [SwaggerOperation("Alle brouwers")]
        public async Task<IActionResult> FindAll() => base.Ok(await repository.FindAllAsync());

        [HttpGet("{id}")]
        //public IActionResult FindById(int id)
        //{
        //    var brouwer = repository.FindById(id);
        //    return brouwer == null? base.NotFound() : base.Ok(brouwer);
        //}

        //verkorte versie
        //url --> brouwers/{id}
        [SwaggerOperation("Brouwer waarvan je de id kent")]
        public async Task<IActionResult> FindById(int id) => 
           await repository.FindByIdAsync(id) is Brouwer brouwer ? base.Ok(brouwer) : base.NotFound();

        
        
        // URl --> brouwers/naam?begin=a --> geeft alle met a in naam
        [HttpGet("naam")]
        [SwaggerOperation("Brouwers waarvan je het begin van de naam kent")]
        public async Task<IActionResult> FindByBeginNaam(string begin) =>
            base.Ok(await repository.FindByBeginNaamAsync(begin));


        /*-----------------------------DELETE--------------------------------------*/

        [HttpDelete("{id}")] 
        [SwaggerOperation("Brouwer verwijderen")]
        public async Task<IActionResult> Delete(int id)
        {
            var brouwer = await repository.FindByIdAsync(id);
            if(brouwer == null)
                return base.NotFound();

            await repository.DeleteAsync(brouwer);
            return base.Ok();
        }

        /*------------------------------POST------------------------------------------*/

        [HttpPost]
        [SwaggerOperation("Brouwer toevoegen")]
        public async Task<IActionResult> Post(Brouwer brouwer)
        {
            if(this.ModelState.IsValid)     //validation
            {
                await repository.InsertAsync(brouwer);
                return base.CreatedAtAction(nameof(FindById), new { id = brouwer.Id }, null);
            }
            return base.BadRequest(ModelState);       //als validation false terug geeft
        }


        /*----------------------------PUT---------------------------------------------*/

        [HttpPut("{id}")]
        [SwaggerOperation("Brouwer wijzigen")]
        public async Task<IActionResult> Put(int id, Brouwer brouwer)
        {
            if (this.ModelState.IsValid)    //validation ok
            {
                try
                {
                    brouwer.Id = id;
                    await repository.UpdateAsync(brouwer);
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
            return base.BadRequest(ModelState);       //validation not okay
        }
    }
}
