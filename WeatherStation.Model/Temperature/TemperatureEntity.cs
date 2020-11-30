namespace WeatherStation.Model.Temperature
{
    using Microsoft.Azure.Cosmos.Table;
    public class TemperatureTableModel : TableEntityBase
    {
        public string Json { get; set; }
    }

    public class TableEntityBase : TableEntity, ITableEntity
    {
        public TableEntityBase()
        {
             

        }
    }
}
