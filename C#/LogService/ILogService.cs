using Application.Models.Log;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example
{
    public interface ILogService
    {
        Task<ICollection<LogModel>> GetLogList(LogDto logDto);
        Task<LogModel> GetLogById(long id);

        Task LogWarning<T>(string message, string detail, Usuarios user);
        Task LogInformation<T>(string message, string detail, Usuarios user);
        Task LogError<T>(string message, string detail, Usuarios user);

        Task LogWarning<T>(string message, string detail);
        Task LogInformation<T>(string message, string detail);
        Task LogError<T>(string message, string detail);
    }
}
