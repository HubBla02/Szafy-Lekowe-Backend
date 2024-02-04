using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SzafyNaLeki.Entities;
using SzafyNaLeki.Models;

namespace SzafyNaLeki.Services
{
    public interface IAlarmService
    {
        Alarm Get();
        void Update();
    }

    public class AlarmService : IAlarmService
    {
        private readonly SzafaDbContext _dbContext;

        public AlarmService(SzafaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Alarm Get()
        {
            var alarm = _dbContext
                .Alarm
                .FirstOrDefault();
            if (alarm == null)
            {
                return null;
            }
            return alarm;
        }

        public void Update()
        {
            var alarmEntity = _dbContext.Alarm.SingleOrDefault();

            var czyAktywny = _dbContext.Szafy.Any(szafa => szafa.Alarm);

            if (czyAktywny && !alarmEntity.Aktywny)
            {
                alarmEntity.Aktywny = true;
                _dbContext.SaveChanges();
            }
            else if (!czyAktywny && alarmEntity.Aktywny)
            {
                alarmEntity.Aktywny = false;
                _dbContext.SaveChanges();
            }
        }
    }
}
