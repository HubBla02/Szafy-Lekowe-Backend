using SzafyNaLeki.Entities;

namespace SzafyNaLeki
{
    public class AlarmBasic
    {
        private readonly SzafaDbContext _dbContext;
        public AlarmBasic(SzafaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Default()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Alarm.Any())
                {
                    var Alarm = Get();
                    _dbContext.Alarm.Add(Alarm);
                    _dbContext.SaveChanges();
                }
            }
        }

        private Alarm Get()
        {
            var czyAktywny = _dbContext.Szafy.Any(szafa => szafa.Alarm);
            var Alarm = new Alarm { Aktywny = czyAktywny } ;
            return Alarm;
        }
    }
}

