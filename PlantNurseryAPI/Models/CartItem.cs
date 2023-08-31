using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantNurseryAPI.Models
{
    public class CartItem
    {
        public int ItemId { get; set; }
        public int ItemTypeId { get; set; }
        public int amount { get; set; }
    }
}