using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace SzafyNaLeki.Entities
{
    public class Szafa
    {
        public int Id {  get; set; }
        public float Temperatura1 { get; set; }
        public float Temperatura2 { get; set; }
        public bool CzyZepsuta { get; set; }
        public bool Alarm {  get; set; }
        public bool CzyAlarm()
        {
            return this.CzyZepsuta|| this.Temperatura1 < 6 || this.Temperatura1 > 10 
                || this.Temperatura2 < 6 || this.Temperatura2 > 10;
        }
    }
}
