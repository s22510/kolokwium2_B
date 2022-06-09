using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Models
{
    public class Musician_Track
    {
        public int idTrack { get; set; }
        public int idMusician { get; set; }

        public virtual Track Track { get; set; }
        public virtual Musician Musician { get; set; }
    }

}
