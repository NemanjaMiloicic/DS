using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Jun24
{
    [DataContract]
    public class Skladiste
    {
        [DataMember]
        public int IdSkladiste { get; set; }

        [DataMember]
        public DateTime PocetakZakupa { get; set; }

        [DataMember]
        public DateTime KrajZakupa { get; set; }

        [DataMember]
        public decimal Cena { get; set; }

    }
}