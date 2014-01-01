using System.Runtime.Serialization;

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