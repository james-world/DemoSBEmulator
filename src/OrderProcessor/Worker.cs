using System.Diagnostics;
using System.Text;

using Azure.Messaging.ServiceBus;

namespace OrderProcessor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await ConsumeMessageFromDefaultQueue();
            await Task.Delay(1000, stoppingToken);
        }
    }
    
    private static async Task ConsumeMessageFromDefaultQueue()
    {
        var connectionString = "Endpoint=sb://127.0.0.1;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";
        string queueName = "orders";

        var client = new ServiceBusClient(connectionString);

        ServiceBusReceiverOptions opt = new ServiceBusReceiverOptions
        {
            ReceiveMode = ServiceBusReceiveMode.PeekLock,
        };

        ServiceBusReceiver receiver = client.CreateReceiver(queueName, opt);

        while (true)
        {
            ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(5));
            if (message != null)
            {
                // Process the message
                Console.WriteLine($"Received message: {Encoding.UTF8.GetString(message.Body)}");

                // Complete the message to remove it from the queue
                await receiver.CompleteMessageAsync(message);
            }
            else
            {
                Console.WriteLine("No messages received.");
                break;
            }
        }

        Console.WriteLine("Done recieving");

        await receiver.DisposeAsync();
        await client.DisposeAsync();
    }    
}
