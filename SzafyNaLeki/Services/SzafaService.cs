using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SzafyNaLeki.Entities;
using SzafyNaLeki.Models;

namespace SzafyNaLeki.Services
{
    public interface ISzafaService
    {
        int Create(UtworzSzafeDto dto);
        IEnumerable<SzafaDto> GetAll();
        SzafaDto GetById(int id);
        bool Delete(int id);
        bool Update(int id, AktualizujSzafeDto dto);
    }
    public class SzafaService : ISzafaService
    {
        private readonly SzafaDbContext _dbContext;
        private readonly IAlarmService _alarmService;
        private readonly IMapper _mapper;
        private readonly ILogger<SzafaService> _logger;

        public SzafaService(SzafaDbContext dbcontext, IMapper mapper, ILogger<SzafaService> logger, IAlarmService alarmService)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
            _logger = logger;
            _alarmService = alarmService;
        }
        public SzafaDto GetById(int id)
        {
            var szafa = _dbContext
                .Szafy
                .FirstOrDefault(x => x.Id == id);
            if (szafa == null)
            {
                return null;
            }
            var result = _mapper.Map<SzafaDto>(szafa);
            return result;
        }

        public IEnumerable<SzafaDto> GetAll()
        {
            var szafy = _dbContext
           .Szafy
           .ToList();
            var result = _mapper.Map<List<SzafaDto>>(szafy);
            return result;
        }

        public int Create(UtworzSzafeDto dto)
        {
            Szafa szafa = _mapper.Map<Szafa>(dto);
            _dbContext.Szafy.Add(szafa);
            _dbContext.SaveChanges();
            _alarmService.Update();
            return szafa.Id;
        }

        public bool Delete(int id) 
        {
            _logger.LogWarning($"Szafa z ID {id} zostanie usunięta!");
            var szafa = _dbContext
                .Szafy
                .FirstOrDefault(x => x.Id == id); 
            if (szafa is null) { return false; }
            _dbContext.Remove(szafa);
            _dbContext.SaveChanges();
            _alarmService.Update();
            return true;
        }

        public bool Update(int id, AktualizujSzafeDto dto)
        {
            var szafa = _dbContext
                .Szafy
                .FirstOrDefault(x => x.Id == id);
            if (szafa is null) { return false; }
            szafa.Temperatura1 = dto.Temperatura1;
            szafa.Temperatura2 = dto.Temperatura2;
            szafa.CzyZepsuta = dto.CzyZepsuta;
            szafa.Alarm = dto.Alarm;
            _dbContext.SaveChanges();
            _alarmService.Update();
            return true;
        }
    }
}
