using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messenger.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    [TypeFilter(typeof(Auth))]
    public class PagesController : Controller
    {
        [TypeFilter(typeof(Access))]
        public IActionResult Chat1()
        {
            return View();
        }
        public IActionResult Chat2()
        {
            return View();
        }
    }
}