using System;


namespace R5T.F0097
{
    public class EmailSender : IEmailSender
    {
        #region Infrastructure

        public static IEmailSender Instance { get; } = new EmailSender();


        private EmailSender()
        {
        }

        #endregion
    }
}
