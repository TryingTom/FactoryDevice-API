using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtteplanMORE.ServiceManual.ApplicationCore.Data;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EtteplanMORE.ServiceManual.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryDeviceMaintenanceController : ControllerBase
    {
        FactoryDeviceDbContext _factoryDeviceDbContext;

        public FactoryDeviceMaintenanceController(FactoryDeviceDbContext factoryDeviceDbContext)
        {
            _factoryDeviceDbContext = factoryDeviceDbContext;
        }

        /// <summary>
        ///     HTTP GET: api/FactoryDeviceMaintenance/
        /// </summary>
        [HttpGet]
        public IActionResult GetMaintenances()
        {
            IQueryable<FactoryDeviceMaintenance> maintenances;

            // here we get all the maintenances,                sorted by most urgent                  then by newest to oldest
            maintenances = _factoryDeviceDbContext.Maintenances.OrderByDescending(q => q.UrgencyLevel).ThenByDescending(q=>q.TimeReported);

            if (maintenances == null)
            {
                return NotFound("Coudn't find any maintenances...");
            }
            else
            {
                return Ok(maintenances);
            }
        }

        /// <summary>
        ///     HTTP GET: api/FactoryDeviceMaintenance/1
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetMaintenance(int id)
        {
            // here we get all the maintenances,                sorted by most urgent                  then by newest to oldest                making sure it exists
            var maintenances = _factoryDeviceDbContext.Maintenances.OrderByDescending(q=>q.UrgencyLevel).ThenByDescending(q => q.TimeReported).FirstOrDefault(m => m.Id == id);
            // we get the device information here, but I'm not sure how to integrate the information to the maintenances...
            //var deviceInformation = _factoryDeviceDbContext.Devices.Find(maintenances.FactoryDeviceId);

            if (maintenances == null)
            {
                return NotFound("No maintenances found...");
            }
            else
            {
                return Ok(maintenances);
            }
        }


        /// <summary>
        ///     HTTP Post: api/FactoryDeviceMaintenance/
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody]FactoryDeviceMaintenance maintenance)
        {
            // if the user gives a FactoryDeviceId which doesn't exist, it leads to an error...
            // I wonder if there's a way to fix that?
            _factoryDeviceDbContext.Maintenances.Add(maintenance);
            _factoryDeviceDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        ///     HTTP Put: api/FactoryDeviceMaintenance/1
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FactoryDeviceMaintenance maintenance)
        {
            var entity = _factoryDeviceDbContext.Maintenances.Find(id);
            // if the entity is not found
            if (entity == null)
            {
                return NotFound("No maintenance found against this id...");
            }
            // if the entity is found
            else
            {
                entity.Id = id;
                entity.TimeReported = maintenance.TimeReported;
                entity.Description = maintenance.Description;
                entity.UrgencyLevel = maintenance.UrgencyLevel;
                entity.Fixed= maintenance.Fixed;

                // if the user gives an FactoryDeviceId which doesn't exist, it leads to an error...
                // maybe if we don't allow the user to change it, it'll fix that problem
                // but would there be a moment where you'd want to change the deviceId?
                //entity.FactoryDeviceId = maintenance.FactoryDeviceId;
                _factoryDeviceDbContext.SaveChanges();
                return Ok("Record updated!");
            }
        }

        /// <summary>
        ///     HTTP Delete: api/FactoryDeviceMaintenance/1
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var maintenance = _factoryDeviceDbContext.Maintenances.Find(id);
            if (maintenance == null)
            {
                return NotFound("No record found against this id...");
            }
            else
            {
                _factoryDeviceDbContext.Maintenances.Remove(maintenance);
                _factoryDeviceDbContext.SaveChanges();
                return Ok("Quote deleteded...");
            }

        }
    }
}