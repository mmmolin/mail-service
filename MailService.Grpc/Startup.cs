using FluentEmail.MailKitSmtp;
using MailService.Core.Entities;
using MailService.Core.Interfaces;
using MailService.Grpc.Services;
using MailService.Infrastructure.Grpc;
using MailService.Infrastructure.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Grpc
{
    public class Startup
    {
        private IConfiguration configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddFluentEmail("test@test.se")
                .AddMailKitSender(new SmtpClientOptions 
                { 
                    UseSsl = false,
                    RequiresAuthentication = true,
                    Server = configuration["MailSettings:Server"],
                    Port = int.Parse(configuration["MailSettings:Port"]),
                    User = configuration["MailSettings:User"],
                    Password = configuration["MailSettings:Password"]
                });

            services.AddScoped<IGrpcClient, GrpcScraperClient>();
            services.AddScoped<IMailRepository, MailRepository>();
            services.Configure<MailSettingOptions>(configuration.GetSection(MailSettingOptions.MailSetting));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<SendMailService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
