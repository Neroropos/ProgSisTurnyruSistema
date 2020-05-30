using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnyruSistema.Models
{
    public class Zaidejas : EntityBase
    {
        public string vardas { set; get; }
        public string slapyvardis { set; get; }
        public Komanda komanda { get; set; }
        public int komandaId { get; set; }
    }

}
