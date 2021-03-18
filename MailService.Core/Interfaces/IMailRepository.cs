using MailService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Core.Interfaces
{
    public interface IMailRepository
    {
        public Task SendMail(IEnumerable<LinkData> linkData);
    }
}
