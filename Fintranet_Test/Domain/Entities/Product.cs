using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fintranet_Test.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string name { get; set; }
        public long price { get; set; }
    }
}
