using FluentEmail.Core;
using MailService.Core.Entities;
using MailService.Core.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailService.Infrastructure.Mail
{
    public class MailRepository : IMailRepository
    {
        private IFluentEmail email;
        private MailSettingOptions mailSettings;
        public MailRepository(IFluentEmail email, IOptions<MailSettingOptions> mailSettings)
        {
            this.email = email;
            this.mailSettings = mailSettings.Value;
        }
        public async Task SendMail(IEnumerable<LinkData> linkData)
        {
            string mailContent = ConvertToMailBody(linkData);

            email.To(mailSettings.EmailAddress)
                .Subject(mailSettings.Subject)
                .Body(mailContent, false);

            await email.SendAsync();
        }

        private string ConvertToMailBody(IEnumerable<LinkData> linkData)
        {
            string mailBody = "";

            foreach(var link in linkData)
            {
                mailBody += $"{link.Title} {link.Url} {Environment.NewLine}";
            }

            return mailBody;
        }
    }
}
