using Kafka.Connector.Models;
using Kafka.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KafkaConsumer.Example
{
    public class CallBack : ICallbackService
    {
        public async Task Message(Message message)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"Mensagem recebida: {Newtonsoft.Json.JsonConvert.SerializeObject(message)}");
            });
        }

        public Task Message(IEnumerable<Message> messages)
        {
            throw new NotImplementedException();
        }
    }
}
