﻿using MailKit.Net.Smtp;
using MimeKit;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Models;
using WeCare.Infrastructure.Configurations;

namespace WeCare.Infrastructure.Services;
public class EmailService : IEmailService
{
    private readonly EmailConfiguration _configuration;

    public EmailService(EmailConfiguration options)
    {
        _configuration = options;
    }

    public async Task SendEmailAsync(EmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message);
        await Send(emailMessage);
    }

    public async Task SendEmailHtmlAsync(EmailMessage message)
    {
        var emailMessage = CreateEmailHtmlMessage(message);
        await Send(emailMessage);
    }

    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_configuration.FromName, _configuration.From));
        emailMessage.To.AddRange(message.To.Select(x => new MailboxAddress(x.Value, x.Key)));
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
        return emailMessage;
    }

    private MimeMessage CreateEmailHtmlMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_configuration.FromName, _configuration.From));
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = message.Content;
        emailMessage.To.AddRange(message.To.Select(x => new MailboxAddress(x.Value, x.Key)));
        emailMessage.Subject = message.Subject;
        emailMessage.Body = bodyBuilder.ToMessageBody();
        return emailMessage;
    }
    private async Task Send(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_configuration.SmtpServer, _configuration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_configuration.UserName, _configuration.Password);
                await client.SendAsync(mailMessage);
            }
            catch(Exception ex)
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}


