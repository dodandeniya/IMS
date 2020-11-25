using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string InventoryName { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
