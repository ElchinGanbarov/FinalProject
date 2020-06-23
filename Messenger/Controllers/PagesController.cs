using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    public class PagesController : Controller
    {
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