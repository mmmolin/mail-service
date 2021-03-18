using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Core.Entities
{
    public record LinkData
    {
        public LinkData(string title, string url)
        {
            Title = title;
            Url = url;
        }

        public string Title { get; init; }
        public string Url { get; init; }
    }
}
