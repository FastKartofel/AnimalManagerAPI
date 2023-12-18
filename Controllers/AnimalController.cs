using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientCareAPI.DAL;
using PatientCareAPI.Models;

namespace PatientCareAPI.Controllers
{
    //for those GET and POST requests the api is: api/animals 

    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }


        [HttpGet]
        public IActionResult Get([FromQuery] string orderBy) {
            return Ok(_animalRepository.GetAnimals(orderBy));
        }


        [HttpPost]
        public IActionResult Post(Animal animal) {
            var createdAnimal = _animalRepository.AddAnimal(animal);

            return Ok(createdAnimal);
        }


        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int idAnimal)
        {
           _animalRepository.DeleteAnimal(idAnimal);
            return Ok("Succsesfully deleted!");
        }


        [HttpPut("{idAnimal}")]
        public IActionResult ChangeAnimal(int idAnimal, [FromBody] Animal animal)
        {
            if (animal._idAnimal != idAnimal)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                _animalRepository.ChangeAnimal(idAnimal, animal);
                return Ok("Successfully changed!");
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }


    }
}