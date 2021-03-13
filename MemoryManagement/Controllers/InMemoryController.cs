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
            _memorycache.Set<string>("currentTime", DateTime.Now.ToString());

            return View();
        }

        public IActionResult GetTime()
        {
            _memorycache.TryGetValue<string>("currentTime", out string mycurrenttime);

            ViewBag.currenttime = mycurrenttime;

            return View();
        }
    }
}
