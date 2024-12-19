using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remembvoc.Models.ApplicationModels
{
    public class Priorities
    {
        public int Id { get; set; }
        public double Points { get; set; }
        public DateTime LastCheck { get; set; }
        public int MinutesToRepeat { get; set; }
        public int Period { get; set; }
        public virtual Words Words { get; set; }
    }
}
