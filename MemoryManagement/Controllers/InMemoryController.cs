using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryManagement.Controllers
{
    public class InMemoryController : Controller
    {
        private IMemoryCache _memorycache;
        public InMemoryController(IMemoryCache memorycache)
        {
            _memorycache = memorycache;
        }

        public IActionResult Index()
        {
            //Set Current Time in currentTime string On IndexPage
            _memorycache.Set<string>("currentTime", DateTime.Now.ToString());

            return View();
        }

        public IActionResult GetTime()
        {
            //in-Memory Control with getorcreate method.
            _memorycache.GetOrCreate<string>("currentTime", newcache => {
                newcache.SetPriority(CacheItemPriority.High);
                newcache.SetSlidingExpiration(TimeSpan.FromMinutes(20));
                return DateTime.Now.ToString(); 
            });

            //try to get value and writing in a string with out parameters 
            _memorycache.TryGetValue<string>("currentTime", out string mycurrenttime);

            //We dont like ViewBagS :)
            ViewBag.currenttime = mycurrenttime;

            return View();
        }
    }
}
