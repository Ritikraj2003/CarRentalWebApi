using CarRentalApi.Models;

namespace CarRentalApi.GmailService
{
    public class GmailBody
    {
        public GmailBody()
        {
          
        }

        public async Task<string> SendEmail(Booking booking)
        {

            string body = $@"
    <html>
    <body>
           <h2>Dear Team<h2>
        <p> Please find the booking request details below.
             request you to please confirm the below booking ans share the cab & Driver Details at the earlist.<p>
        <table border='1' cellpadding='8' cellspacing='0' style='border-collapse: collapse; width: 100%;'>
            <thead>
                <tr style='background-color: #f2f2f2;'>
                    <th>Field</th>
                    <th>Details</th>
                </tr>
            </thead>
            <tbody>
                <tr><td>BookingId</td><td>{booking.BookingId}</td></tr>
                <tr><td>Name</td><td>{booking.Name}</td></tr>
                <tr><td>Email</td><td>
            {booking.Email}</td></tr>
                <tr><td>Phone</td><td>{booking.Phone_no}</td></tr>
                <tr><td>Car Type</td><td>{booking.cartype}</td></tr>
                <tr><td>Booking Type</td><td>{booking.BookingType}</td></tr>
                <tr><td>Pickup Location</td><td>{booking.PickupLocation}</td></tr>
                <tr><td>Pickup Date</td><td>{booking.PickupDate}</td></tr>
                <tr><td>Pickup Time</td><td>{booking.PickupTime}</td></tr>
                <tr><td>Drop Location</td><td>{booking.DropLocation}</td></tr>
                <tr><td>Drop Date</td><td>{booking.Dropdate}</td></tr>
                <tr><td>Drop Time</td><td>{booking.Droptime}</td></tr>
            </tbody>
        </table>

        <p style='color: yellow; background-color: black; font-weight: bold; padding: 10px; margin-top: 20px;'>
            Auto generated email
        </p>
    </body>
    </html>";
            return body;
        }

        public async Task<string> SendEmailToUser(Booking booking)
        {

            string subject = "Booking Confirmation";
            string Body = $@"
<html>
<body>
    <h2>Dear {booking.Name},</h2>
    <p>Your booking has been successfully received.</p>
    <p><strong>Booking ID:</strong> {booking.BookingId}</p>
    <p>Please wait for a confirmation email with driver details.</p>
    <p>Thank you for choosing our service.</p>

    <p style='color: yellow; background-color: black; font-weight: bold; padding: 10px; margin-top: 20px;'>
        Auto generated email
    </p>

    <br/>
    <p>Best regards,</p>
    <p><strong>Car Rental Team</strong><br/>
    contact@yourcompany.com<br/>
    +91-9876543210</p>
</body>
</html>";
            return Body; ;
        }

    }
}
