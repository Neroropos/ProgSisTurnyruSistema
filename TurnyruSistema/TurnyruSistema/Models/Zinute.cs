using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnyruSistema.Models
{
    public class Zinute:EntityBase
    {
        public string Tema { get; set; }
        public string Turinys { get; set; }
        public DateTime IssiuntimoData { get; set; }
        public Naudotojas Naudotojas { get; set; }
        public int NaudotojasId { get; set; }
    }
}
