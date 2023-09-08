using PlantNurseryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PlantNurseryAPI.Controllers
{
    public class ValuesController : ApiController
    {
        DatabaseOperator _db = DatabaseOperator.Instance;

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAllItemTypes()
        {
            var ItemTypes = _db.GetListOfItems();
            if (ItemTypes == null)
            {
                return NotFound();
            }
            return Ok(ItemTypes);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult GetNurseryItemsByType(int id )
        {
            
            try
            {
                var nurseryItems = _db.GetListOfItems().Where(items => items.ItemTypeID == id).FirstOrDefault().Items;
                if (nurseryItems == null)
                {
                    return NotFound();
                }
                return Ok(nurseryItems);
            }
            catch
            {
                return NotFound();
            }
            
            
        }
        
        [System.Web.Http.HttpPost]
        public IHttpActionResult UpdateItemsCount([FromBody] CartItem item)
        {
            var itemType = _db.GetListOfItems().Where(it => it.ItemTypeID == item.ItemTypeId).FirstOrDefault();
            var NurseryItem = itemType.Items.Where(it => it.NurseryID == item.ItemId).FirstOrDefault();
            if (NurseryItem == null)
            {
                return NotFound();
            }
            NurseryItem.Amount = NurseryItem.Amount - item.amount;
            _db.UpdateItem(NurseryItem);

            var cartItem = new NurseryItems()
            {
                NurseryID = NurseryItem.NurseryID,
                Name = NurseryItem.Name,
                Amount = item.amount,
                Price = NurseryItem.Price,
                ItemType = NurseryItem.ItemType,
            };
            return Ok(cartItem);
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult Restore()
        {
            _db.ResetItemCount();
            return Ok();
        }

    }
}
