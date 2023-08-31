using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantNurseryAPI.Models
{
    public sealed class DatabaseOperator
    {
        private static DatabaseOperator instance = null;
        private  IEnumerable<ItemType> ListOfItems;
        private static readonly object padlock = new object();

        private DatabaseOperator()
        {
            this.ListOfItems = DatabaseInitializer.GetAllItems();
        }

        public static DatabaseOperator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DatabaseOperator();
                        }
                    }
                }
                return instance;
            }
        }

        public IEnumerable<ItemType> GetListOfItems()
        {
            return this.ListOfItems;
        }

        public void UpdateItem(NurseryItems item)
        {
            var itemType = ListOfItems.Where(it => it.ItemTypeID == item.ItemType.ItemTypeID).FirstOrDefault();
            var nurseryItem = itemType.Items.Where(it => it.NurseryID == item.NurseryID).FirstOrDefault();
            nurseryItem.Amount = item.Amount;
        }

        public void ResetItemCount()
        {
            this.ListOfItems = DatabaseInitializer.GetAllItems();
        }
    }
}