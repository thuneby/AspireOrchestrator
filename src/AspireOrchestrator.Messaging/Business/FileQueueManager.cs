using AspireOrchestrator.Core.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AspireOrchestrator.Messaging.Business
{
    public class FileQueueManager<T1, T2>(ILoggerFactory loggerFactory)
        where T1 : GuidModelBase
        where T2 : GuidModelBase
    {
        public string MqPath { get; set; } = @"C:\Temp\MQ";

        private readonly ILogger _logger = loggerFactory.CreateLogger<FileQueueManager<T1, T2>>();
        private static readonly object Lock = new object();

        public bool Put(T1 entity, string queueName)
        {
            var fileName = entity.Id + ".json";
            try
            {
                var queuePath = GetFullPath(queueName);
                CreateDirectory(queuePath);
                var fullPath = Path.Combine(queuePath, fileName);
                var payload = JsonSerializer.Serialize(entity);
                lock (Lock)
                {
                    File.WriteAllTextAsync(fullPath, payload);
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
            return true;
        }

        public async Task<bool> PutAll(List<T1> entities, string queueName)
        {
            var queuePath = GetFullPath(queueName);
            CreateDirectory(queuePath);
            try
            {
                foreach (var entity in entities)
                {
                    var fileName = entity.Id + ".json";
                    var fullPath = Path.Combine(queuePath, fileName);
                    var payload = JsonSerializer.Serialize(entity);
                    await File.WriteAllTextAsync(fullPath, payload);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return false;
            }
            return true;
        }

        public T2? Get(string queueName)
        {
            var payload = string.Empty;
            var fullPath = GetFullPath(queueName);
            if (!Directory.Exists(fullPath))
                return null;
            lock (Lock)
            {
                var firstMessage = Directory.GetFiles(fullPath).FirstOrDefault();
                if (firstMessage == null) return null;
                payload = File.ReadAllText(firstMessage);
                File.Delete(firstMessage);
            }

            var entity = JsonSerializer.Deserialize<T2>(payload);
            return entity;
        }

        public int GetQueueLength(string queueName)
        {
            var fullPath = GetFullPath(queueName);
            if (!Directory.Exists(fullPath))
                return 0;
            var count = Directory.GetFiles(fullPath).Length;
            return count;
        }

        private string GetFullPath(string queueName)
        {
            return Path.Combine(MqPath, queueName);
        }

        private static void CreateDirectory(string fullPath)
        {
            lock (Lock)
            {
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
            }
        }
    }
}
