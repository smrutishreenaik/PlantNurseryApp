using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantNurseryAPI.Models
{
    public static class DatabaseInitializer
    {
        public static IEnumerable<ItemType> GetAllItems()
        {
            IList<ItemType> items = new List<ItemType>();
            var treeType = new ItemType()
            {
                ItemTypeID = 1,
                Name = "Trees"
            };
            var shrubType = new ItemType()
            {
                ItemTypeID = 2,
                Name = "Shrubs"
            };
            var flowerType = new ItemType()
            {
                ItemTypeID = 3,
                Name = "Flowers"
            };

            //TreeType
            treeType.Items = new List<NurseryItems>();
            treeType.Items.Add(new NurseryItems
            {
                NurseryID = 1,
                Name = "Banyan Tree",
                Amount = 12,
                Price = 20,
                ItemType = treeType,
            });
            treeType.Items.Add(new NurseryItems
            {
                NurseryID = 2,
                Name = "Coconut Tree",
                Amount = 15,
                Price = 10,
                ItemType = treeType,
            });
            treeType.Items.Add(new NurseryItems
            {
                NurseryID = 3,
                Name = "Fig Tree",
                Amount = 20,
                Price = 50,
                ItemType = treeType,
            });

            //ShrubType
            shrubType.Items = new List<NurseryItems>();
            shrubType.Items.Add(new NurseryItems
            {
                NurseryID = 1,
                Name = "Rhododendrons",
                Amount = 17,
                Price = 40,
                ItemType = shrubType,
            });
            shrubType.Items.Add(new NurseryItems
            {
                NurseryID = 2,
                Name = "Myrtles",
                Amount = 18,
                Price = 10,
                ItemType = shrubType,
            });
            shrubType.Items.Add(new NurseryItems
            {
                NurseryID = 3,
                Name = "Boxwood",
                Amount = 10,
                Price = 50,
                ItemType = shrubType,
            });

            //FlowerType
            flowerType.Items = new List<NurseryItems>();
            flowerType.Items.Add(new NurseryItems
            {
                NurseryID = 1,
                Name = "Petunia",
                Amount = 10,
                Price = 20,
                ItemType = flowerType,
            });
            flowerType.Items.Add(new NurseryItems
            {
                NurseryID = 2,
                Name = "Marigold",
                Amount = 18,
                Price = 10,
                ItemType = flowerType,
            });
            flowerType.Items.Add(new NurseryItems
            {
                NurseryID = 3,
                Name = "Zinnia",
                Amount = 10,
                Price = 30,
                ItemType = flowerType,
            });

            items.Add(treeType);
            items.Add(shrubType);
            items.Add(flowerType);

            return items;
        }
    }
}
