using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMongo.Models
{
    public class Note
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Flavor")]
        public string Flavor { get; set; }
        [BsonElement("Iamge")]
        public string Iamge { get; set; }
        [BsonElement("Rate")]
        public int Rate { get; set; }
        [BsonElement("Text")]
        public string Text { get; set; }
        [BsonElement("StoreName")]
        public string StoreName { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Contact")]
        public string Contact { get; set; }
        //[BsonElement("IceCream")]
       
    }
    
}