using Catalog.Abstraction.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Abstraction.Models
{
    [DataContract]
    public class Product
    {
        public Product()
        {

        }
        public Product(ProductEntity entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Description = entity.Description;
            this.Category = entity.Category;
            this.Price = entity.Price;
            this.Summary = entity.Summary;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]
        public string Id { get; set; }
        [BsonElement("Name")]
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Summary { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string ImageFile { get; set; }
        [DataMember]
        public decimal Price { get; set; }
    
    }
}
