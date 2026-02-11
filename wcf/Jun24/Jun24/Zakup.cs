using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Jun24
{
    [DataContract]
    public class Zakup
    {
        [DataMember]
        public Vlasnik Vlasnik { get; set; }
        [DataMember]
        public Skladiste Skladiste { get; set; }
    }
}