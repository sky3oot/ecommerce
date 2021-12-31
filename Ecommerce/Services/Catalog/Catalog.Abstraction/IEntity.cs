using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Abstraction
{
    public interface IEntity
    {
        public bool IsDeleted { get; set; }
  
        public string Id { get; set; }
    }
}
