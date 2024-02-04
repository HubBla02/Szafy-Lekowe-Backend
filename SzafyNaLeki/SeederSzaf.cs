using SzafyNaLeki.Entities;

namespace SzafyNaLeki
{
    public class SeederSzaf
    {
        private readonly SzafaDbContext _dbContext;
        public SeederSzaf(SzafaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Szafy.Any())
                {
                    var Szafy = GetSzafy();
                    _dbContext.Szafy.AddRange(Szafy);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Szafa> GetSzafy()
        {
            var szafy = new List<Szafa>()
            {
                new Szafa()
                {
                    Temperatura1 = 7,
                    Temperatura2 = 9,
                    CzyZepsuta = false
                },

                new Szafa()
                {
                    Temperatura1 = 8,
                    Temperatura2 = 9,
                    CzyZepsuta = false
                }
            };

            foreach (var szafa in szafy)
            {
                szafa.Alarm = szafa.CzyAlarm();
            }

            return szafy;
        }
    }
}
