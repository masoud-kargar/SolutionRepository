﻿using AllInterfaces;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace WebRepository.Areas.CatrgoryArea.Controllers {
    public class Categories : Controller {

        public IActionResult Index() {
            return View();
        }
    }
}
