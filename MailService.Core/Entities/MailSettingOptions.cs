using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Core.Entities
{
    public class MailSettingOptions
    {
        public const string MailSetting = "MailSettings";

        public string EmailAddress { get; set; }
        public string Subject { get; set; }
    }
}
