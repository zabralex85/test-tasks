using Medialink.Domain;

namespace Medialink.RepositoryInterfaces
{
    public interface ILoggerRepository
    {
        int Log(RequestLogDbModel requestLog);
    }
}
