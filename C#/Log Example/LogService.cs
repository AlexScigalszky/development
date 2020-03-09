using Example.Model;
using Example.Model.Repository;
using Example.Shared.DTOs;
using System.Collections.Generic;
using AutoMapper;

namespace Example.Services
{
    public class LogService
    {
        private readonly ILogDataRepository<LogDTO, Log> LogManager;
        private readonly IMapper Mapper;

        public LogService(ILogDataRepository<LogDTO, Log> LogManager, IMapper Mapper)
        {
            this.LogManager = LogManager;
            this.Mapper = Mapper;
        }

        public Log Get(long LogId)
        {
            return LogManager.Get(LogId);
        }

        public IEnumerable<LogDTO> GetAll()
        {
            return LogManager.FindByConditionDTO(x => true);
        }

        public void Add(LogDTO Log)
        {
            var entity = Mapper.Map<LogDTO,Log>(Log);
            LogManager.Add(entity);
            LogManager.SaveChanges();
        }

    }

}
