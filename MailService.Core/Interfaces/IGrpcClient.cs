using MailService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Core.Interfaces
{
    public interface IGrpcClient
    {
        public Task<IEnumerable<LinkData>> CallService();
    }
}
