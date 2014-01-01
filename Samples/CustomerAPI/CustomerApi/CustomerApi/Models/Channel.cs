using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CustomerApi.Models
{
    [DataContract]
    public class Channel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Uri { get; set; }
    }

}