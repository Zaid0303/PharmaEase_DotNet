using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DotNet_Project.Models
{
    public class PurchasesViewModel
    {
        public int Id { get; set; }

        public List<DateTime> DateTime { get; set; }
        public string ImageLink { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int? Price { get; set; }

        public string Description { get; set; }

        public int ProductId { get; set; }
    }
}