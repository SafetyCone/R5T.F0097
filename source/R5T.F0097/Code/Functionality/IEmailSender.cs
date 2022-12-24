using System;
using System.Net;
using System.Net.Mail;

using R5T.T0132;
using R5T.T0144;


namespace R5T.F0097
{
    [FunctionalityMarker]
    public partial interface IEmailSender : IFunctionalityMarker
    {
        /// <summary>
        /// Sends a message, filling in the from-address and providing the authentication.
        /// </summary>
        /// <param name="message"></param>
        public void SendEmail(MailMessage message)
        {
            var gmailAuthenticationFilePath = Instances.FilePaths.GmailAuthenticationFilePath;

            var authentication = Instances.JsonOperator.LoadFromFile_Synchronous<Authentication>(
                gmailAuthenticationFilePath,
                Instances.JsonKeys.GmailAuthentication);

            var credentials = new NetworkCredential(
                authentication.Username,
                authentication.Password);

            var fromAddress = authentication.Username;
            var displayName = "David-Automation";

            message.From = new MailAddress(
                fromAddress,
                displayName);

            using var smtpClient = new SmtpClient()
            {
                Host = @"smtp.gmail.com",
                Port = 587,
                Credentials = credentials,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };

            smtpClient.Send(message);
        }
    }
}
