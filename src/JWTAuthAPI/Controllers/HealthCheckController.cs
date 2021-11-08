using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JWTAuthAPI.Controllers
{
    public class HealthCheckController : Controller
    {
        // GET: HealthCheck
        public string Index()
        {
            return "Health";
        }
    }
}