﻿using DemoApi.Hubs;
using DemoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHubContext<SignalHub> _hubContext;
        public HomeController(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("PushEmployee")]
        public IActionResult PushEmployee(int Id, string Name, string Address)
        {
            Employee employee = new Employee();
            employee.Id = Id;
            employee.Name = Name;
            employee.Address = Address;

            _hubContext.Clients.All.SendAsync("ReceiveEmployee", employee);
            return Ok("Done");
        }

        [HttpGet]
        [Route("PushMessage")]
        public IActionResult PushMessage(string Message)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", Message);
            return Ok("Done");
        }
    }
}
