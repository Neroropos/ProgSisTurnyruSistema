using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnyruSistema.Models
{
    public class KompiuteriuZona : EntityBase
    {
        public string Pavadinimas { set; get; }
        public int KompiuteriuSkaicius { set; get; }
        public Turnyras Turnyras { set; get; }
        public int TurnyrasId { set; get; }
        public List<Zaidimas> Zaidimai { get; set; }
    }
}
