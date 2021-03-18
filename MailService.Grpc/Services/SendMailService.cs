using Grpc.Core;
using MailService.Core.Entities;
using MailService.Core.Interfaces;
using MailService.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Grpc.Services
{
    public class SendMailService : SendMail.SendMailBase
    {
        private readonly IGrpcClient grpcClient;
        private readonly IMailRepository mailRepository;

        public SendMailService(IGrpcClient grpcClient, IMailRepository mailRepository)
        {
            this.grpcClient = grpcClient;
            this.mailRepository = mailRepository;
        }

        public override async Task<SendMailReply> RunService(SendMailRequest request, ServerCallContext context)
        {
            try
            {
                IEnumerable<LinkData> linkData = await grpcClient.CallService();
                if(CollectionHasData(linkData))
                {
                    await mailRepository.SendMail(linkData);
                }
            }
            catch
            {
                //Log
            }
            return await Task.FromResult(new SendMailReply());
        }

        private bool CollectionHasData<T>(IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }
    }
}
