﻿using DSC.Auth.API.Application.Messages.Commands.UserCommand;
using DSC.Core.Mediator;
using DSC.MessageBus;
using DSC.MessageBus.Integration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSC.Auth.API.Application.Messages.ConsumersBus
{
    public class CreateUserConsumer : BackgroundService, ICreateUserConsumer
    {

        private readonly IMessageBusService _messageBusService;
        private readonly IServiceProvider _serviceProvider;

        public CreateUserConsumer(IMessageBusService messageBusService, IServiceProvider serviceProvider)
        {
            _messageBusService = messageBusService;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageBusService.Subscribe(QueueType.NEW_USER, RegisterConsumer);
            return Task.CompletedTask;
        }

        public void RegisterConsumer(BasicDeliverEventArgs message)
        {
            var byteArray = message.Body.ToArray();
            var messageString = Encoding.UTF8.GetString(byteArray);
            var user = JsonConvert.DeserializeObject<CreateUserIntegrationEvent>(messageString);

            // Eu crio um scopo pois esta classe foi injetada com AddHo
            using (var scope = _serviceProvider.CreateScope())
            {

                foreach (var item in user.Guardians)
                {
                    var command = new AddUserCommand
                    {
                        Email = item.Email,
                        Password = item.Password,
                        Phone = item.Phone
                    };
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                    var t = Task.Run(() => mediator.SendCommand(command));
                    t.Wait();
                }

                // Emite o evento para criação dos usuários dos guardiões
                var created = new UserCreatedOkIntegrationEvent(user.Id);                
                _messageBusService.Publish(QueueType.CREATED_OK, user);

            }
        }
    }
}