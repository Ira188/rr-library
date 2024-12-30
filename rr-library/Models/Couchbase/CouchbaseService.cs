using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.KeyValue;
using Couchbase.Transactions.Config;
using Couchbase.Transactions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rr_library.Models.Couchbase
{
    public interface ICouchbaseService
    {
        ICluster Cluster { get; }
        IBucket MainBucket { get; }
        public Task<ICouchbaseCollection> GetCollection(string scopename, string collection);
        public Transactions CreateTx();
    }

    public static class StringExtension
    {
        public static string DefaultIfEmpty(this string str, string defaultValue)
            => string.IsNullOrWhiteSpace(str) ? defaultValue : str;
    }

    public class CouchbaseService : ICouchbaseService
    {
        public ICluster Cluster { get; private set; }
        public IBucket MainBucket { get; private set; }
        public ILogger<CouchbaseService> Logger { get; }

        public CouchbaseService(IClusterProvider clusterProvider, ILogger<CouchbaseService> logger)
        {
            Logger = logger;
            Logger.LogInformation("Connecting to couchbase");

            try
            {
                var task = Task.Run(async () =>
                {
                    var cluster = await clusterProvider.GetClusterAsync();

                    Cluster = cluster;
                    MainBucket = await Cluster.BucketAsync(Environment.GetEnvironmentVariable("Cb_BucketName")!);
                });
                task.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle((x) => throw x);
            }
            if (Cluster == null)
            {
                throw new Exception("Cluster is null");
            }
            if (MainBucket == null)
            {
                throw new Exception("MainBucket is null");
            }
        }

        public async Task<ICouchbaseCollection> GetCollection(string scopename, string collection)
        {
            var scope = await MainBucket.ScopeAsync(scopename);
            return await scope.CollectionAsync(collection);
        }
        public Transactions CreateTx()
        {
            return Transactions.Create(Cluster, TransactionConfigBuilder.Create());
        }
    }
}
