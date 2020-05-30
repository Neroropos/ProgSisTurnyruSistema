using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnyruSistema.Models
{
    public class Raundas:EntityBase
    {
        public int Numeris { get; set; }
        public Turnyras Turnyras { get; set; }
        public int TurnyrasId { get; set; }
        public List<Zaidimas> Zaidimai { get; set; }
    }
}
