using Grpc.Net.Client;
using System.Collections.Generic;
using System.Net.Http;
using MailService.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
using MailService.Infrastructure.Grpc.Protos;
using MailService.Core.Interfaces;

namespace MailService.Infrastructure.Grpc
{
    public class GrpcScraperClient : IGrpcClient
    {
        public async Task<IEnumerable<LinkData>> CallService()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Grpc.Protos.Mail.MailClient(channel);
            var reply = await client.GetMailAsync(new Protos.MailRequest());
            var links = reply.Links.Select(x => new LinkData(x.Title, x.Url));
            return await Task.FromResult(links);
        }
    }
}
