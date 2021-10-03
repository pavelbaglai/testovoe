using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication39.Models
{
    public class Katalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Opisanie { get; set; }
        public int Shena { get; set; }
        public string PrimO { get; set; }
        public string PrimS { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Katalog> Katalogs { get; set; }
        public Category()
        {
            Katalogs = new List<Katalog>();
        }
    }
}