using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RedisApp.Controllers
{
    public class RedisController : Controller
    {
        private IDistributedCache _distributedCache;
        public RedisController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {

            DistributedCacheEntryOptions opt = new DistributedCacheEntryOptions();
            opt.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            // Classic Method

            //_distributedCache.SetString("namesurname", "Ömer SAĞLAM", opt);

            // Asyncronous method
            await _distributedCache.SetStringAsync("namesurname", "Ömer SAĞLAM", default);

            Customer newcustomer = new Customer() { ID = 1, CustomerName = "Customer1", CustomerDesc = "Customer Description", CreatedDate = DateTime.Now };

            string mycustomer_json = JsonConvert.SerializeObject(newcustomer);

            await _distributedCache.SetStringAsync("mycustomer", mycustomer_json, opt);

            return View();
        }

        public async Task<IActionResult> GetValue()
        {
            //ViewBag.str = _distributedCache.GetString("namesurname");

            ViewBag.str = await _distributedCache.GetStringAsync("namesurname");

            string json_customer = await _distributedCache.GetStringAsync("mycustomer").ConfigureAwait(false);
            if(!String.IsNullOrEmpty(json_customer))
            { 
                Customer customer = JsonConvert.DeserializeObject<Customer>(json_customer);
                if (customer != null)
                    ViewBag.Customername = customer.CustomerName;
            }
            return View();
        }

        public async Task<IActionResult> ClearKey()
        {
            //_distributedCache.Remove("namesurname");

            await _distributedCache.RemoveAsync("namesurname");
            return View();
        }

        public IActionResult SetImage()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/image1.jpg");
            byte[] imagebytes = System.IO.File.ReadAllBytes(path);
            _distributedCache.Set("showimage", imagebytes);

            return View();
        }

        public IActionResult ShowImage()
        {
            byte[] imagebytes = _distributedCache.Get("showimage");
            
            return File(imagebytes, "image/jpg");
        }
    }
}
