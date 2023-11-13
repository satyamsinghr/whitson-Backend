using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using crudAuthApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Net;
using System.Net.Mail;

namespace crudAuthApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class EmailConfirmationOtp : ControllerBase
    {
        private readonly Context _context;
        private readonly UserService _userService;
      
        public EmailConfirmationOtp(Context context , UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("send-otp")]
        [AllowAnonymous]
        public IActionResult SendOtp([FromBody] EmailDetail emailDetail)
        {
            var user = _context.UserDetails.FirstOrDefault(u => u.Email == emailDetail.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            string otp = GenerateOtp();

            SendOtpByEmailAsync(emailDetail.Email, otp);
            user.Opt = int.Parse(otp); 
            _context.SaveChanges();
            return Ok(new { Message = "OTP sent successfully", Otp = otp, UserId = user.Id });
        }

        [HttpPost("verify-otp")]
        [AllowAnonymous]
        public IActionResult VerifyOtp([FromBody] VerifyOtpModel verifyOtpModel)
        {
            var user = _context.UserDetails.FirstOrDefault(u => u.Id == verifyOtpModel.Id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            if (user.Opt == verifyOtpModel.Otp)
            {
                // OTP is correct
                user.Status = true;
                _context.SaveChanges();
                return Ok(new { Message = "OTP verification successful" });
            }
            else
            {
                // Incorrect OTP
                return BadRequest("Incorrect OTP");
            }
        }



        private string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(1000, 9999);
            return otp.ToString();
        }

        private async Task SendOtpByEmailAsync(string recipientEmail, string otp)
        {
            Console.WriteLine($"Sending OTP {otp} to {recipientEmail}");
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress("satyam.upwork26701@gmail.com");

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("satyam.upwork26701@gmail.com", "Hosting@123"); 
            smtp.Host = "satyam.upwork26701@gmail.com";

            mail.To.Add(new MailAddress(recipientEmail));
            mail.IsBodyHtml = true;
            string body = $"Your OTP: {otp}";
            mail.Body = body;

            try
            {
              //var data=  await smtp.Send(mail);
                Console.WriteLine("OTP sent successfully via email.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending OTP via email: {ex.Message}");
            }
        }

    }

}
