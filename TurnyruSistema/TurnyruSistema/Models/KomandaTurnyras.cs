using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnyruSistema.Models
{
    public class KomandaTurnyras:EntityBase
    {
        public bool Dalyvauja { get; set; }
        public int Ispejimai { get; set; }
        public Komanda Komanda { get; set; }
        public int KomandaId { get; set; }
        public Turnyras Turnyras { get; set; }
        public int TurnyrasId { get; set; }
    }
}
