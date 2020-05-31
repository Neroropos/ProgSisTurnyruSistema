using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnyruSistema.Models
{
    public class Turnyras : EntityBase
    {
        public string Pavadinimas { get; set; }
        public string Vieta { get; set; }
        public DateTime PradziosData { get; set; }
        public DateTime PabaigosData { get; set; }
        public Organizatorius Organizatorius { get; set; }
        public List<Raundas> Raundai { get; set; }
        public int OrganizatoriusId { get; set; }
        public List<KompiuteriuZona> KompiuteriuZonos { get; set; }
        public List<KomandaTurnyras> Komandos { get; set; }
    }
}
