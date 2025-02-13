﻿using Microsoft.AspNetCore.Mvc;
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

            //AbsoluteExpression : Cache LifeCycle
            //SlidingExpression : With regenerate the cache every time it is used

            if (_memorycache.TryGetValue<string>("currentTime", out string mycurrenttime)) {
                MemoryCacheEntryOptions option = new MemoryCacheEntryOptions();
                //AbsoluteExpression... START
                option.AbsoluteExpiration = DateTime.Now.AddSeconds(5);
                //AbsoluteExpression... FINISH

                //SlidingExpression.... START
                option.SlidingExpiration = TimeSpan.FromSeconds(5);
                //SlidingExpression.... FINISH

                option.RegisterPostEvictionCallback((key,val,reason,state) =>
                {
                    _memorycache.Set("removecachereason", $"{key}-{val} : {reason}");
                });

                _memorycache.Set<string>("currentTime", DateTime.Now.ToString(), option);
            }

            return View();
        }

        public IActionResult GetTime()
        {
            
            //in-Memory Control with GetOrCreate method.
            _memorycache.GetOrCreate<string>("currentTime", newcache => {
                newcache.SetPriority(CacheItemPriority.High);
                newcache.SetSlidingExpiration(TimeSpan.FromMinutes(20));
                return DateTime.Now.ToString(); 
            });

            //try to get value and writing in a string with out parameters 
            _memorycache.TryGetValue<string>("currentTime", out string mycurrenttime);
            _memorycache.TryGetValue<string>("removecachereason", out string removecachereason);

            //We dont like ViewBagS :)
            ViewBag.currenttime = mycurrenttime;
            ViewBag.removecachereason = removecachereason;

            return View();
        }
    }
}
