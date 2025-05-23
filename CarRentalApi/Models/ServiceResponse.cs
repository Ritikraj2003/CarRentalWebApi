

namespace CarRentalApi.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; } 
        public bool Success {  get; set; }  
        
        public String Message {  get; set; }    
    }
}
