using ConfigServerHost.Core.Domain;
using System.Threading.Tasks;

namespace ConfigServerHost.Core.Interfaces
{
    public interface IProcessHandlerAsync<in T, R>
    {
        Task<OperationResult<R>> HandleAsync(T request);
    }
}
