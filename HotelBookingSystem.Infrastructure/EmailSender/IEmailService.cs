﻿

namespace HotelBookingSystem.Infrastructure.EmailSender
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);

    }
}
