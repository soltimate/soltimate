using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoltimateLib.Services
{
    /// <summary>
    /// This class is used by the application to send emails for various purposes.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
