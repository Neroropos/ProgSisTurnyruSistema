using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TurnyruSistema.Models
{
    public enum Busena
    {
        Laukiama,
        Zaidziama,
        Pasibaiges
    }

    public class Zaidimas:EntityBase
    {
        public DateTime Laikas { get; set; }
        public Busena Busena { get; set; }
        
        public Komanda Komanda1 { get; set; }
        //[ForeignKey("Komanda1")]
        public int? Komanda1Id { get; set; }
        
        public Komanda Komanda2 { get; set; }
        //[ForeignKey("Komanda2")]
        public int? Komanda2Id { get; set; }
        public int LaimejusiKomanda { get; set; }
        public KompiuteriuZona KompiuteriuZona { get; set; }
        public int? KompiuteriuZonaId { get; set; }
        public Raundas Raundas { get; set; }
        public int? RaundasId { get; set; }

    }
}
