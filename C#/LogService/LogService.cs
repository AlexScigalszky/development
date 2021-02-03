using Example.Interfaces;
using Example.Mapper;
using Example.Interfaces;
using Example.Constants;
using Example.Entities;
using Example.Repositories;
using Example.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
        }

        public async Task<LogModel> GetLogById(long id)
        {
            var spec = new LogSpecification(id);
            var list = await _logRepository.GetAsync(spec);
            var mapped = ObjectMapper.Mapper.Map<LogModel>(list);
            return mapped;
        }

        public async Task<ICollection<LogModel>> GetLogList(LogDto logDto)
        {
            var spec = new LogSpecification(logDto.Id, logDto.Message, logDto.Module, logDto.level, logDto.TimeStamp, logDto.UserId);
            var list = await _logRepository.GetAsync(spec);
            var mapped = ObjectMapper.Mapper.Map<ICollection<LogModel>>(list);
            return mapped;
        }

        public async Task LogWarning<T>(string message, string detail)
        {
            try
            {
                await Log<T>(LogConstants.WARNING, message, detail, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging {LogConstants.WARNING}: " + ex.Message);
            }
        }

        public async Task LogInformation<T>(string message, string detail)
        {
            try
            {
                await Log<T>(LogConstants.INFO, message, detail, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging {LogConstants.INFO}: " + ex.Message);
            }
        }

        public async Task LogError<T>(string message, string detail)
        {
            try
            {
                await Log<T>(LogConstants.ERROR, message, detail, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging {LogConstants.ERROR}: " + ex.Message);
            }
        }

        public async Task LogWarning<T>(string message, string detail, user user)
        {
            try
            {
                await Log<T>(LogConstants.WARNING, message, detail, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging {LogConstants.WARNING}: " + ex.Message);
            }
        }

        public async Task LogInformation<T>(string message, string detail, user user)
        {
            try
            {
                await Log<T>(LogConstants.INFO, message, detail, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging {LogConstants.INFO}: " + ex.Message);
            }
        }

        public async Task LogError<T>(string message, string detail, user user)
        {
            try
            {
                await Log<T>(LogConstants.ERROR, message, detail, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging {LogConstants.ERROR}: " + ex.Message);
            }
        }

        private async Task Log<T>(string level, string message, string detail, user user)
        {
            Type typeParameterType = typeof(T);
            Log log = GetLogObject(level, message, detail, typeParameterType, user);
            await _logRepository.AddAsync(log);
        }

        private Log GetLogObject(string level, string message, string detail, Type typeParameterType, user user)
        {
            var log = new Log()
            {
                Level = level,
                TimeStamp = DateTime.Now,
                Module = typeParameterType.Name,
                Detail = detail,
                Message = message
            };
            if (user != null)
                log.IdUsuario = user.Idusuario;
            return log;
        }

    }
}
