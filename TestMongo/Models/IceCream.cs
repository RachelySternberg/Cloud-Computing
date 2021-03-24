using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMongo.Models
{
    public class IceCream
    {

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Image")]
        public string Image { get; set; }

        [BsonElement("Rating")]
        public int Rating { get; set; }

        [BsonElement("Calories")]
        public double Calories { get; set; }

        [BsonElement("Fats")]
        public double Fats { get; set; }

        [BsonElement("Protein")]
        public double Protein { get; set; }

        [BsonElement("NutritionalValues")]
        public string NutritionalValues { get; set; }

    }
}