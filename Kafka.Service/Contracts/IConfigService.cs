using Kafka.Connector.Models;
using System.Threading.Tasks;

namespace Kafka.Service.Contracts
{
    public interface IConfigService
    {
        Task SetCustomConfig(BasicConfiguration configProperties);
    }
}
