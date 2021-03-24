using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMongo.Models
{
    public class Store
    {

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Adress")]
        public string Adress { get; set; }

        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("Images")]
        public string Images { get; set; }

        [BsonElement("IceCreams")]
        public List<IceCream> IceCreams { get; set; } = new List<IceCream>();

    }
}