using EmailPractice.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailPractice.Controllers
{
    public class EmailController : Controller
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Send()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(string toEmail,string subject,string body,IFormFile file)
        {
            await _emailService.SendAndSaveAsync(toEmail, subject, body, file);

            TempData["Msg"] = "Email sent and saved successfully!";
            return RedirectToAction("Send");
        }
    }
}
