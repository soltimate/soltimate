using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoltimateLib.Services
{

    /// <summary>
    /// Interface for the application to send emails for various purposes.
    /// </summary>
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
