﻿//using EasyNetQ;
//using Serilog.Context;
//using System.Threading;
//using System.Threading.Tasks;
//using EasyNetQ.AutoSubscribe;
//using Microsoft.Extensions.Logging;
//using CharlieBackend.Core.DTO.EmailData;
//using CharlieBackend.Core.IntegrationEvents.Events;
//using WhatBackend.EmailRenderService.Services.Interfaces;

//namespace WhatBackend.EmailRenderService.IntegrationEvents.EventHandling
//{
//    public class AccountApprovedConsumer : IConsumeAsync<AccountApprovedEvent>
//    {

//        private readonly ILogger<AccountApprovedConsumer> _logger;
//        private readonly IBus _bus;
//        private readonly IMessageTemplateService _messageTemplate;

//        public AccountApprovedConsumer(ILogger<AccountApprovedConsumer> logger, IBus bus,
//                                       IMessageTemplateService messageTemplate)
//        {
//            _logger = logger;
//            _bus = bus;
//            _messageTemplate = messageTemplate;
//        }

//        public async Task ConsumeAsync(AccountApprovedEvent message, CancellationToken cancellationToken = default)
//        {
//            using (LogContext.PushProperty("IntegrationEventContext", $"{message}"))
//            {

//                _logger.LogInformation($"Account has been approved: {message}");

//                _logger.LogInformation("-----Publishing AccountApprovedEvent integration event----- ");

//                await _bus.SendReceive.SendAsync("EmailSenderService", new EmailData
//                                                 {
//                                                     RecipientMail = message.RecepientMail,
//                                                     EmailBody = _messageTemplate.AccountApprovedTemplate(message)
//                                                 });
//            }
//        }
//    }
//}
