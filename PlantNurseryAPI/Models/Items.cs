using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlantNurseryAPI.Models
{
    public class NurseryItems
    {
        [Key]
        public int NurseryID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public ItemType ItemType { get; set; }
    }
    public class ItemType
    {
        [Key]
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public IList<NurseryItems> Items { get; set; }
    }
}