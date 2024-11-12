using Data.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.VehicleClasses
{
    [Table("Vehicle_class")]
    public class Vehicle_Class : DataBaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
