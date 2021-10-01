using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
 

namespace WebApplication39.Models
{
    public class KatalogListViewModel
    {
        public IEnumerable<Katalog> Katalogs { get; set; }
        public SelectList Categories { get; set; }
        public string Name { get; set; }
    }
}