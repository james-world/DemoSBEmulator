// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Create the logging configuration
var logging = new Logging("File");

// Initialize the user configuration
var config = new UserConfig(logging)
{
    Namespaces = new List<Namespace>
    {
        new Namespace("sbemulatorns")
            .AddQueue(new Queue("queue.1"))
            .AddTopic(
                new Topic("topic.1")
                    .AddSubscription(
                        new Subscription("subscription.1")
                            .AddRule(new Rule("app-prop-filter-1", new RuleProperties
                            {
                                CorrelationFilter = new CorrelationFilter
                                {
                                    ContentType = "application/json"
                                }
                            }))
                    )
                    .AddSubscription(
                        new Subscription("subscription.2")
                            .AddRule(new Rule("user-prop-filter-1", new RuleProperties
                            {
                                CorrelationFilter = new CorrelationFilter
                                {
                                    Properties = new Dictionary<string, string>
                                    {
                                        { "prop1", "value1" }
                                    }
                                }
                            }))
                    )
                    .AddSubscription(new Subscription("subscription.3"))
            )
    }
};

// Serialization options
var options = new JsonSerializerOptions
{
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};

// Serialize the configuration to JSON
string jsonString = JsonSerializer.Serialize(config, options);

// Output the JSON string
Console.WriteLine(jsonString);
