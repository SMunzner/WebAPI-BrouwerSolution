using BrouwerService.DTO;
using BrouwerService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BrouwerService.Controllers
{
    [Route("filialen")]
    [ApiController]
    public class FiliaalController(IFiliaalRepository repository) : ControllerBase
    {
        [HttpGet, SwaggerOperation("Alle filialen")]    //geef naam voor /swagger
        public async Task<ActionResult> FindAll()
        {
            var filialen = await repository.FindAllAsync();

            var filiaalDTOs = filialen.Select(filiaal => new FiliaalDTO
            (
                filiaal.Id,
                filiaal.Naam,
                filiaal.Woonplaats.Postcode,
                filiaal.Woonplaats.Naam
            ));
            return base.Ok(filiaalDTOs);
        }
            
    }
}
