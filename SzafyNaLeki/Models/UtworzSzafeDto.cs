using System.ComponentModel.DataAnnotations;

namespace SzafyNaLeki.Models
{
    public class UtworzSzafeDto
    {
        [Required]
        private float temperatura1;
        [Required]
        private float temperatura2;
        [Required]
        public bool CzyZepsuta = false;
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

        public bool Alarm { get; private set; }

        private void UstawAlarm()
        {
            Alarm = Temperatura1 < 6 || Temperatura1 > 10 || Temperatura2 < 6 || Temperatura2 > 10 || CzyZepsuta;
        }
    }
}
