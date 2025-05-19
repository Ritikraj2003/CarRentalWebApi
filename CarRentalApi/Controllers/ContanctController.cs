using System.Threading.Tasks;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContanctController : ControllerBase
    {
        private readonly IContactRepository contactRepository;

        public ContanctController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContact()
        {
            var res= await contactRepository.GetAllAsync();
            return Ok(res);
        }
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetContact(Guid id)
        {
            var res = await contactRepository.GetByIdAsync(id);
            return Ok(res);
        }

        [HttpPost]
       public async Task<IActionResult> CreateContact( Contact contact)
        {
             var res = await contactRepository.CreateAsync(contact);

            return CreatedAtAction(nameof(GetContact), new { id = res.Id }, res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var deletedContact = await contactRepository.DeleteAsync(id);
            if (deletedContact == null)
            {
                return NotFound(); 
            }

            return Ok(deletedContact); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(Guid id, Contact contact)
        {
            if ( contact.Id == null)
            {
                return BadRequest("Contact ID Not found");
            }
            var updatedContact = await contactRepository.UpdateAsync(contact);
            if (updatedContact == null)
            {
                return NotFound();
            }
            return Ok(updatedContact);
        }

    }
}
