using System.Net;
using System.Net.Mail;

namespace PhotoBooth.BL.Facades
{
    public static class EmailFacade
    {
        public static void SendEmail(string recipient, string password)
        {
            var smtpClient = new SmtpClient("smtp.websupport.sk")
            {
                Port = 25,
                Credentials = new NetworkCredential("webapp@smileshoot.sk", "Password1*"),
                EnableSsl = false,
                Timeout = 10000
            };

            smtpClient.Send("webapp@smileshoot.sk", recipient, "Account registration", "Credentials:\nname: " + recipient + "\npassword: " + password);
        }
    }
}