using System.Threading.Tasks;
using CarRentalApi.Interface;
using CarRentalApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Authorize]
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
            try
            {
                var res = await contactRepository.GetAllAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var res = await contactRepository.GetByIdAsync(id);
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpPost]
       public async Task<IActionResult> CreateContact( Contact contact)
        {
            try
            {
                var res = await contactRepository.CreateAsync(contact);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var deletedContact = await contactRepository.DeleteAsync(id);
                if (deletedContact == null)
                {
                    return NotFound();
                }
                return Ok(deletedContact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            try
            {
                var updatedContact = await contactRepository.UpdateAsync(id, contact);
                if (updatedContact == null)
                {
                    return NotFound();
                }
                return Ok(updatedContact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
