using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisApp.Controllers
{
    public class RedisController : Controller
    {
        public IActionResult Index()
        {
            //Redis Value Types
            //String
            //List
            //Set
            //Sorted Set
            //Hash
            return View();
        }
    }
}
