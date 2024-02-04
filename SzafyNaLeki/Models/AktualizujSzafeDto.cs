using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SzafyNaLeki.Models
{
    public class AktualizujSzafeDto
    {
        [Required]
        private float temperatura1;
        [Required]
        private float temperatura2;
        [Required]
        public bool CzyZepsuta;
        public bool Alarm { get; private set; }

        public float Temperatura1
        {
            get => temperatura1;
            set
            {
                temperatura1 = value;
                UstawAlarm();
            }
        }

        public float Temperatura2
        {
            get => temperatura2;
            set
            {
                temperatura2 = value;
                UstawAlarm();
            }
        }

        public bool czyZepsuta
        {
            get => CzyZepsuta;
            set
            {
                CzyZepsuta = value;
                UstawAlarm();
            }
        }


        private void UstawAlarm()
        {
            Alarm = Temperatura1 < 6 || Temperatura1 > 10 || Temperatura2 < 6 || Temperatura2 > 10 || czyZepsuta;
        }
    }
}
