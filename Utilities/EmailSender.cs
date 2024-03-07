using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        //logic to send e-mail
        return Task.CompletedTask;
    }
}