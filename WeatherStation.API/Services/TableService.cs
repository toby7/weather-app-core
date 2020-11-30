using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation.API.Services
{
    using System.Reflection.Metadata.Ecma335;
    using System.Threading;
    using Microsoft.Azure.Cosmos.Table;
    using Microsoft.Azure.Cosmos.Table.Queryable;
    using Microsoft.Extensions.Configuration;
    using Model.Temperature;
    using Newtonsoft.Json;

    public class TableService : ITableService
    {
        private readonly IConfiguration configuration;

        public TableService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //public IEnumerable<T> GetLatestEntities<T>(string tableName) where T : TableEntityBase
        //{
        //    var conString = this.configuration.GetValue<string>("ConnectionStrings:TableStorage");

        //    var storageAccount = CloudStorageAccount.Parse(conString);
        //    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
        //    var hej = TableOperation.Retrieve<T>();

        //    var tjo = new TableQuery();
        //    //CloudTable table = tableClient .GetTableReference("thankfulfor");
        //    var table = tableClient.GetTableReference(tableName);
        //    //var query = table.CreateQuery<T>().
        //    var entities = table.ExecuteQuery(new TableQuery<TemperatureTableModel>().Take(1)).ToList();

        //    return entities.AsEnumerable();
        //}

        public async Task<string> GetLatestEntities(string tableName)
        {
            throw new Exception("table service??+");
            var conString = this.configuration.GetValue<string>("ConnectionStrings:TableStorage");

            var storageAccount = CloudStorageAccount.Parse(conString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            var tjo = new TableQuery();
            //CloudTable table = tableClient .GetTableReference("thankfulfor");
            var table = tableClient.GetTableReference(tableName);
            var query = table.CreateQuery<TemperatureTableModel>()
                .AsQueryable()
                .Take(1)
                .AsTableQuery();

            var entities = await query.ExecuteAsync(CancellationToken.None);

            //var test = JsonConvert.DeserializeObject<TemperatureModel>(entities.FirstOrDefault().Json);

            var model = entities.FirstOrDefault()?.Json ?? string.Empty;
            //var entities = await table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());

            return model;
        }

        //var conString = this.configuration.GetValue<string>("ConnectionStrings:TableStorage");

        //var storageAccount = CloudStorageAccount.Parse(conString);
        //CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

        //var tjo = new TableQuery();
        ////CloudTable table = tableClient .GetTableReference("thankfulfor");
        //var table = tableClient.GetTableReference(tableName);
        //var query = table.CreateQuery<TemperatureTableModel>()
        //    .AsQueryable()
        //    .Take(1)
        //    .AsTableQuery();

        //var entities = await query.ExecuteAsync(CancellationToken.None);

        ////var test = JsonConvert.DeserializeObject<TemperatureModel>(entities.FirstOrDefault().Json);

        //var model = entities.FirstOrDefault()?.Json ?? string.Empty;
        //    //var entities = await table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());

        //    return model; //entities.AsEnumerable();
    }

    public interface ITableService
    {
        Task<string> GetLatestEntities(string tableName);
    }

    public static class QueryExtensions
    {
        public static async Task<IEnumerable<TElement>> ExecuteAsync<TElement>(this TableQuery<TElement> tableQuery, CancellationToken ct)
        {
            var nextQuery = tableQuery;
            var continuationToken = default(TableContinuationToken);
            var results = new List<TElement>();

            do
            {
                //Execute the next query segment async.
                var queryResult = await nextQuery.ExecuteSegmentedAsync(continuationToken, ct);

                //Set exact results list capacity with result count.
                results.Capacity += queryResult.Results.Count;

                //Add segment results to results list.
                results.AddRange(queryResult.Results);

                continuationToken = queryResult.ContinuationToken;

                //Continuation token is not null, more records to load.
                if (continuationToken != null && tableQuery.TakeCount.HasValue)
                {
                    //Query has a take count, calculate the remaining number of items to load.
                    var itemsToLoad = tableQuery.TakeCount.Value - results.Count;

                    //If more items to load, update query take count, or else set next query to null.
                    nextQuery = itemsToLoad > 0
                        ? tableQuery.Take<TElement>(itemsToLoad).AsTableQuery()
                        : null;
                }

            } while (continuationToken != null && nextQuery != null && !ct.IsCancellationRequested);

            return results;
        }
    }
}
