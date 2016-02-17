using System;
using System.Runtime.Serialization;

namespace Inventory.Data.Model
{
    [DataContract]
    public class Item
    {

        [DataMember]
        public int Id;

        [DataMember]
        public string Label;

        [DataMember]
        public DateTime Expiration;

    }
}