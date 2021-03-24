using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestMongo.Models
{
    public class Opinions
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Opinion")]
        public string Opinion { get; set; }

        [BsonElement("IceCream")]
        public string Icecream { get; set; }

        [BsonElement("Store")]
        public string Store { get; set; }

        [BsonElement("Image")]
        public string Image { get; set; }

        [DataType(DataType.Date)]

        [BsonElement("Date")]
        public DateTime Date { get; set; }
        public Opinions(Store store)
        {
            Opinion = "";
            Date = DateTime.Now;
            Store = store.Id.ToString();
            Icecream = "";
        }
        public Opinions()
        {
            Opinion = "";
            Date = DateTime.Now;
        }


    }

}