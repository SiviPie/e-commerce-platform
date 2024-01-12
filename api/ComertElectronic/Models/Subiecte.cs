using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Subiecte
    {
        public Subiecte()
        {
            Postaris = new HashSet<Postari>();
        }

        public int IdSubiect { get; set; }
        public string NumeSubiect { get; set; } = null!;
        public string? DescriereSubiect { get; set; }

        public virtual ICollection<Postari> Postaris { get; set; }
    }
}
