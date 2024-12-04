using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public record UserConfig
{
    public List<Namespace> Namespaces { get; init; } = new();
    public Logging Logging { get; init; }

    public UserConfig()
    {
        Logging = new Logging("Console");
    }

    public UserConfig(Logging logging)
    {
        Logging = logging;
    }
}

public record Namespace
{
    public string Name { get; init; }
    public List<Queue> Queues { get; init; } = new();
    public List<Topic> Topics { get; init; } = new();

    public Namespace(string name)
    {
        Name = name;
    }

    public Namespace AddQueue(Queue queue)
    {
        Queues.Add(queue);
        return this;
    }

    public Namespace AddTopic(Topic topic)
    {
        Topics.Add(topic);
        return this;
    }
}

public record Queue
{
    public string Name { get; init; }
    public QueueProperties Properties { get; init; }

    public Queue(string name, QueueProperties? properties = null)
    {
        Name = name;
        Properties = properties ?? new QueueProperties();
    }
}

public record QueueProperties
{
    public bool DeadLetteringOnMessageExpiration { get; init; } = false;

    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public TimeSpan DefaultMessageTimeToLive { get; init; } = TimeSpan.FromDays(14);

    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public TimeSpan DuplicateDetectionHistoryTimeWindow { get; init; } = TimeSpan.FromMinutes(1);

    public string ForwardDeadLetteredMessagesTo { get; init; } = "";

    public string ForwardTo { get; init; } = "";

    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public TimeSpan LockDuration { get; init; } = TimeSpan.FromSeconds(30);

    public int MaxDeliveryCount { get; init; } = 10;

    public bool RequiresDuplicateDetection { get; init; } = false;

    public bool RequiresSession { get; init; } = false;
}

public record Topic
{
    public string Name { get; init; }
    public TopicProperties Properties { get; init; }
    public List<Subscription> Subscriptions { get; init; } = new();

    public Topic(string name, TopicProperties? properties = null)
    {
        Name = name;
        Properties = properties ?? new TopicProperties();
    }

    public Topic AddSubscription(Subscription subscription)
    {
        Subscriptions.Add(subscription);
        return this;
    }
}

public record TopicProperties
{
    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public TimeSpan DefaultMessageTimeToLive { get; init; } = TimeSpan.FromDays(14);

    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public TimeSpan DuplicateDetectionHistoryTimeWindow { get; init; } = TimeSpan.FromMinutes(1);

    public bool RequiresDuplicateDetection { get; init; } = false;
}

public record Subscription
{
    public string Name { get; init; }
    public SubscriptionProperties Properties { get; init; }
    public List<Rule> Rules { get; init; } = new();

    public Subscription(string name, SubscriptionProperties? properties = null)
    {
        Name = name;
        Properties = properties ?? new SubscriptionProperties();
    }

    public Subscription AddRule(Rule rule)
    {
        Rules.Add(rule);
        return this;
    }
}

public record SubscriptionProperties
{
    public bool DeadLetteringOnMessageExpiration { get; init; } = false;

    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public TimeSpan DefaultMessageTimeToLive { get; init; } = TimeSpan.FromDays(14);

    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public TimeSpan LockDuration { get; init; } = TimeSpan.FromSeconds(30);

    public int MaxDeliveryCount { get; init; } = 10;

    public string ForwardDeadLetteredMessagesTo { get; init; } = "";

    public string ForwardTo { get; init; } = "";

    public bool RequiresSession { get; init; } = false;
}

public record Rule
{
    public string Name { get; init; }
    public RuleProperties Properties { get; init; }

    public Rule(string name, RuleProperties? properties = null)
    {
        Name = name;
        Properties = properties ?? new RuleProperties();
    }
}

public record RuleProperties
{
    public string FilterType { get; init; } = "Correlation";
    public CorrelationFilter CorrelationFilter { get; init; } = new();
}

public record CorrelationFilter
{
    public string? ContentType { get; init; }
    public string? CorrelationId { get; init; }
    public string? Label { get; init; }
    public string? MessageId { get; init; }
    public string? ReplyTo { get; init; }
    public string? ReplyToSessionId { get; init; }
    public string? SessionId { get; init; }
    public string? To { get; init; }
    public Dictionary<string, string> Properties { get; init; } = new();
}

public record Logging
{
    public string Type { get; init; }

    public Logging(string type)
    {
        Type = type;
    }
}
