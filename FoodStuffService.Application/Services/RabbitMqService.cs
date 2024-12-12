using System.Text;
using System.Text.Json;
using FoodStuffService.Domain.Entities;
using FoodStuffService.Domain.Interfaces;
using FoodStuffService.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace FoodStuffService.Application.Services;

public class RabbitMqService : IMessageService
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly string _exchangeName;
    private readonly string _queueName;
    private readonly string _routingKey;
    
    public RabbitMqService(IConnectionFactory factory, IServiceProvider serviceProvider)
    {
        _connectionFactory = factory;

        var config = serviceProvider.GetRequiredService<IConfiguration>();
        var rabbitConfiguration = config.GetSection("RabbitMq");
        _exchangeName = rabbitConfiguration.GetValue<string>("ExchangeName") ?? "";
        _queueName = rabbitConfiguration.GetValue<string>("Queue") ?? "";
        _routingKey = rabbitConfiguration.GetValue<string>("RoutingKey") ?? "";
    }

    public async Task PublishMessageAsync<T>(Message<T> message)
    {
        await using var connection = await _connectionFactory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        
        await channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Direct);
        await channel.QueueDeclareAsync(_queueName, false, false, false, null);
        await channel.QueueBindAsync(_queueName, _exchangeName, _routingKey, null);
        
        var messageAsJson = JsonSerializer.Serialize(message);
        var msgAsBytes = Encoding.UTF8.GetBytes(messageAsJson);
        
        await channel.BasicPublishAsync(_exchangeName, _routingKey, false, msgAsBytes);

        await channel.CloseAsync();
        await connection.CloseAsync();
    }
    
}