﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitHubUserSearch.UI.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Search()
        {
            return View();
        }
    }
}