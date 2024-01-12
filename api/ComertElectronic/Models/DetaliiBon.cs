using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class DetaliiBon
    {
        public int IdDetaliuBon { get; set; }
        public int IdBon { get; set; }
        public int IdProdus { get; set; }
        public int Cantitate { get; set; }
        public int Pret { get; set; }
    }
}
