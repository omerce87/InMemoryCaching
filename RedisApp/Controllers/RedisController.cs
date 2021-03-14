using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
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
        public IActionResult Index()
        {

            DistributedCacheEntryOptions opt = new DistributedCacheEntryOptions();
            opt.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            _distributedCache.SetString("namesurname", "Ömer SAĞLAM", opt);

            return View();
        }

        public IActionResult GetValue()
        {
            ViewBag.str = _distributedCache.GetString("namesurname");
            return View();
        }
    }
}
