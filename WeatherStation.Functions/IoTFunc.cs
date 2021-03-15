namespace WeatherStation.Functions
{
    using System;
    using System.Text;
    using Microsoft.Azure.EventHubs;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using WeatherStation.Model.Temperature;
    using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

    public static class IoTFunc
    {
        [FunctionName("IoTFunc")]
        [return: Table("Temperature", Connection = "StorageConnectionString")]
        public static TemperatureTableModel Run([IoTHubTrigger("messages/events", Connection = "ConnectionString")] EventData message, ILogger log)
        {
            log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");
            return new TemperatureTableModel { PartitionKey = "Temperature", RowKey = (DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks).ToString("d19"), Json = Encoding.UTF8.GetString(message.Body.Array) };
        }
    }
}