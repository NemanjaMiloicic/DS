using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace April25
{
    [DataContract]
    public class Rezultat
    {
        [DataMember]
        public decimal Vrednost { get; set; }

        [DataMember]
        public string Izraz {  get; set; }
    }
}