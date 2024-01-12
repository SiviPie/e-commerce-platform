using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Administratori
    {
        public int IdAdministrator { get; set; }
        public int IdUtilizator { get; set; }

        public virtual Utilizatori IdUtilizatorNavigation { get; set; } = null!;
    }
}
