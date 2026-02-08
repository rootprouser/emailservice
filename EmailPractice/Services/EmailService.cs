using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail;
namespace EmailPractice.Services
{
    public class EmailService
    {
        public async Task SendAndSaveAsync(string toEmail, string subject, string body, IFormFile file)
        {
            byte[]? fileBytes = null;
            if (file != null && file.Length > 0)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }
            MailMessage mail = new MailMessage("modidhrumil6@gmail.com", toEmail, subject, body);
            if (fileBytes != null)
            {
                mail.Attachments.Add(new Attachment(new MemoryStream(fileBytes), file.FileName));
            }
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("modidhrumil6@gmail.com", "lkdifjiuvnhinqzi"),
                EnableSsl = true
            };
            await smtp.SendMailAsync(mail);
            using SqlConnection con = new SqlConnection("Server=DHRUMIL_MODI\\SQLEXPRESS;Database=practice2;User Id=sa;" +
                "Password=admin@123;Encrypt=True;TrustServerCertificate=True;Pooling=false;");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO EmailLog(ToEmail, Subject, Body, FileName, FileData)VALUES 
                                    (@To, @Sub, @Body, @File, @Data)", con);
            cmd.Parameters.AddWithValue("@To", toEmail);
            cmd.Parameters.AddWithValue("@Sub", subject);
            cmd.Parameters.AddWithValue("@Body", body);
            cmd.Parameters.AddWithValue("@File", (object?)file?.FileName ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Data", (object?)fileBytes ?? DBNull.Value);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
