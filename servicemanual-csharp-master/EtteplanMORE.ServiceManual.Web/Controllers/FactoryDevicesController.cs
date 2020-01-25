using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Data;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryDevicesController : Controller
    {
        FactoryDeviceDbContext _factoryDeviceDbContext;

        public FactoryDevicesController(FactoryDeviceDbContext factoryDeviceDbContext)
        {
            _factoryDeviceDbContext = factoryDeviceDbContext;
        }
        
        /// <summary>
        ///     HTTP GET: api/factorydevices/
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            // it returns the devices also with maintenance needs. If there's no need for maintenance, it'll return null on that
            var devices = _factoryDeviceDbContext.Devices.Include("Maintenances");
            if (devices == null)
            {
                return NotFound("Couldn't find any devices...");
            }
            else
            {
                return Ok(devices);
            }
        }

        /// <summary>
        ///     HTTP GET: api/factorydevices/1
        /// </summary>
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var device = _factoryDeviceDbContext.Devices.Include("Maintenances").FirstOrDefault(m => m.Id == id);
            if (device == null)
            {
                return NotFound("No devices found...");
            }
            else
            {
                return Ok(device);
            }
        }
    }
}