using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Task1.ServiceModel
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [DataMember]
        public int id { get; set; }

        public string name { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public double price { get; set; }
    }
}
