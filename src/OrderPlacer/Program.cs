using Azure.Messaging.ServiceBus;

const string _connectionString = "Endpoint=sb://127.0.0.1;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=SAS_KEY_VALUE;UseDevelopmentEmulator=true;";

await PublishMessageToDefaultQueue();

static async Task PublishMessageToDefaultQueue() 
{
    const int numOfMessagesPerBatch = 10;
    const int numOfBatches = 10;

    string queueName = "orders";

    var client = new ServiceBusClient(_connectionString);
    var sender = client.CreateSender(queueName);

    for (int i = 1; i <= numOfBatches; i++)
    {
        using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

        for (int j = 1; j <= numOfMessagesPerBatch; j++)
        {
            messageBatch.TryAddMessage(new ServiceBusMessage($"Batch:{i}:Message:{j}"));
        }
        await sender.SendMessagesAsync(messageBatch);
    }

    await sender.DisposeAsync();
    await client.DisposeAsync();

    Console.WriteLine($"{numOfBatches} batches with {numOfMessagesPerBatch} messages per batch has been published to the queue.");
}