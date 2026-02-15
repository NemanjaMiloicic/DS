using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Jan25
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string FromUser { get; set; }
        [DataMember]
        public string ToUser { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public DateTime SentAt { get; set; }
    }
}