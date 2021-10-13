using System.Runtime.Serialization;

namespace AxessTask.API.Contracts
{
    [DataContract]
    public class NorthwindCategory
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
