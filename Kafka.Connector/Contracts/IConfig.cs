using System.Threading.Tasks;

namespace Kafka.Connector.Contracts
{
    public interface IConfig
    {
        Task ExecuteServerConfig();
    }
}
