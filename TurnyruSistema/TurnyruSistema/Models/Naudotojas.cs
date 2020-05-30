using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnyruSistema.Models
{
    public class Naudotojas : EntityBase
    {
        public string Prisijungimas { get; set; }
        public string Slaptazodis { get; set; }
        public string ElPastas { get; set; }
        public DateTime RegistracijosData { get; set; }
        public List<Zinute> Zinutes { get; set; }
    }
}
