using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController(IGenericRepository<Pet> repo) : ControllerBase
    {

        [HttpGet("{id:int}")]      // api/pet/2
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var pet = await repo.GetByIdAsync(id);

            if (pet == null) return NotFound();

            return pet;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pet>>> GetPets(string? type, char? gender, string? sort)
        {
            var spec = new PetSpecification(type, gender, sort);
            var pets = await repo.ListAsync(spec);
            // if (pets == null) return NotFound();

            return Ok(pets);
        }

        // [HttpGet]
        // public async Task<IReadOnlyList<Pet>> GetPetsWithSpec(string? type, char? gender, string? sort)
        // {
        //     var spec = new PetSpecification(type, gender, sort);

        //     return await repo.GetPetsWithSpec(spec);
        // }

        [HttpPost]
        public async Task<ActionResult<Pet>> AddPet(Pet pet)
        {
            repo.Add(pet);

            if (await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetPet", new { id = pet.Id }, pet);
            }

            return BadRequest("Pet not added");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Pet>> UpdatePet(int id, Pet pet)
        {
            if (pet.Id != id || !PetExists(id))
            {
                return BadRequest("Cannot update record");
            }

            repo.Update(pet);

            if (await repo.SaveAllAsync())
            {
                return Ok(pet);
            }
            return BadRequest("Cannot update record");

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePet(int id)
        {
            var pet = await repo.GetByIdAsync(id);
            if (pet == null) return NotFound();

            repo.Remove(pet);

            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Cannot delete record");

        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await repo.ListAsync(spec));
        }

        private bool PetExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
