using Newtonsoft.Json;
using NLog;
using PlantNurseryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace PlantNurseryAPI.Controllers
{
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string Baseurl = "http://localhost:5300/";

        public async Task<ActionResult> Index()
        {
            List<ItemType> ItemTypes = new List<ItemType>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/values/GetAllItemTypes");

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    ItemTypes = JsonConvert.DeserializeObject<List<ItemType>>(Response);
                }
                else
                {
                    logger.Info("IHttpActionResult NotFound at Index" + Environment.NewLine + DateTime.Now);
                    return View("Error");
                }

                logger.Info("Hello You have visited the Index view" + Environment.NewLine + DateTime.Now);
                return View(ItemTypes);
            }
        }
        public async Task<ActionResult> NurseryItemList(int id)
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"]?? "";

            List<NurseryItems> nurseryItems = new List<NurseryItems>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/values/GetNurseryItemsByType/"+id);

                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    nurseryItems = JsonConvert.DeserializeObject<List<NurseryItems>>(Response);
                }
                else
                {
                    logger.Info("IHttpActionResult NotFound at NurseryItemList" + Environment.NewLine + DateTime.Now);
                    return View("Error");
                }

                return View(nurseryItems);
            }
        }

        public async Task<ActionResult> AddToCart(int ItemId, int ItemTypeId, int amount)
        {
            NurseryItems cartItem = new NurseryItems();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var item = new CartItem()
                {
                    ItemId = ItemId,
                    ItemTypeId = ItemTypeId,
                    amount = amount,
                };
                HttpResponseMessage response = await client.PostAsJsonAsync("api/values/UpdateItemsCount", item);

                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    cartItem = JsonConvert.DeserializeObject<NurseryItems>(Response);
                    TempData["SuccessMessage"] = "Item added to cart";
                }
                else
                {

                    logger.Info("IHttpActionResult NotFound at Add to cart" + Environment.NewLine + DateTime.Now);
                    return View("Error");
                }
            }

            if (Session["cart"] == null)
            {
                IList<NurseryItems> cart = new List<NurseryItems>();
                cart.Add(cartItem);
                Session["cart"] = cart;
            }
            else
            {
                List<NurseryItems> cart = (List<NurseryItems>)Session["cart"];
                cart.Add(cartItem);
                Session["cart"] = cart;
            }
            logger.Info("Item added to cart" + Environment.NewLine + DateTime.Now);
            return RedirectToAction("NurseryItemList", new { id = ItemTypeId });
        }
        public ActionResult Cart()
        {
            IList<NurseryItems> cartItems;
            if (Session["cart"] == null)
            {
                cartItems = new List<NurseryItems>();
            }
            else
            {
                cartItems = (List<NurseryItems>)Session["cart"];
            }
            return View(cartItems);
        }
        public async Task<ActionResult> Restore()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/values/Restore");

                if (!Res.IsSuccessStatusCode)
                {
                    logger.Info("IHttpActionResult NotFound at Restore" + Environment.NewLine + DateTime.Now);
                    return View("Error");
                }
                logger.Info("Database restored" + Environment.NewLine + DateTime.Now);
                return RedirectToAction("Index");
            }           
        }
        public async Task<ActionResult> EmptyCart()
        {
            IList<NurseryItems> cartItems;
            if (Session["cart"] != null)
            {
                cartItems = (List<NurseryItems>)Session["cart"];
                cartItems.Clear();
                Session["cart"] = cartItems;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage Res = await client.GetAsync("api/values/Restore");

                    if (!Res.IsSuccessStatusCode)
                    {
                        logger.Info("IHttpActionResult NotFound at Restore" + Environment.NewLine + DateTime.Now);
                        return View("Error");
                    }
                }
            }
            logger.Info("Emptied cart" + Environment.NewLine + DateTime.Now);
            return RedirectToAction("Index");
        }
    }
}
