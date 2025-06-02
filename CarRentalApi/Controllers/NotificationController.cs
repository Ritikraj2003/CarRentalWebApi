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
    public class NotificationController : ControllerBase
    {
        private readonly INotification notification;

        public NotificationController(INotification notification)
        {
            this.notification = notification;
            // Constructor logic can be added here if needed
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] Notification notification)
        {
            try
            {
                if (notification == null)
                {
                    return BadRequest("Notification cannot be null.");
                }
                var response = await this.notification.AddNotification(notification);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificationById(int id)
        {
            try
            {
                var response = await notification.DeleteNotification(id);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotificationsAsync()
        {
            try
            {
                var response = await notification.GetAllNotification();
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationByIdAsync(int id)
        {
            try
            {
                var response = await notification.GetNotificationById(id);
                if (response.Success)
                {
                    return Ok(response.Data);
                }
                else
                {
                    return NotFound(response.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       [HttpPut("{id}")]
            public async Task<IActionResult> UpdateNotification(int id, [FromBody] Notification notification)
            {
                try
                {
                   
                    var response = await this.notification.UpdateNotification( id,notification);
                    if (response.Success)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound(response.Message);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
       
    }

  }


