using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Entity;

namespace scr.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly List<Order> _orders = new List<Order>() { };
        [HttpGet]
        public ActionResult getOrders()
        {
            return Ok(_orders);
        }
        [HttpPost]
        public ActionResult setOrder()
        {
            return Ok(_orders);
        }
    }
}