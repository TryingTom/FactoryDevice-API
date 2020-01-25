using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EtteplanMORE.ServiceManual.ApplicationCore.Data
{
    public class FactoryDeviceDbContext : DbContext
    {
        public FactoryDeviceDbContext(DbContextOptions<FactoryDeviceDbContext> options) : base(options)
        {

        }

        public DbSet<FactoryDevice> Devices { get; set; }
        public DbSet<FactoryDeviceMaintenance> Maintenances { get; set; }
    }
}
