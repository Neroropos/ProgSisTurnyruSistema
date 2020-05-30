using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TurnyruSistema.Models
{
    public class Komanda:Naudotojas
    {
        public int Laimejimai { get; set; }
        public int Pralaimejimai { get; set; }
        public string Paveikslelis { get; set; }
        public string pavadinimas { get; set; }
        public List<Zaidejas> zaidejai { get; set; }
        public List<KomandaTurnyras> Turnyrai { get; set; }
        [InverseProperty("Komanda1")]
        public List<Zaidimas> Zaidimai1 { get; set; }
        [InverseProperty("Komanda2")]
        public List<Zaidimas> Zaidimai2 { get; set; }
    }
}
