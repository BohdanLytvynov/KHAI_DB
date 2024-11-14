using Data.Attributes.Bindings;
using Data.Models.Base;
using Data.Models.Engines;
using Data.Models.Nations;
using Data.Models.Tank_Turrets;
using Data.Models.VehicleClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.Tanks
{
    public class Tank : DataBaseEntity<int>
    {        
        public string Name { get; set; } = string.Empty;

        [OneToMany("Vehicle_Class_Id")]        
        public Vehicle_Class Vehicle_Class { get; set; } = default;

        public float Strength { get; set; }

        public float Armor { get; set; }

        public float Max_Velocity { get; set; }

        public float Velocity_rotate { get; set; }

        public uint Tank_Level { get; set; }

        public Nation Nation { get; set; } = default; 

        public List<Tank_Turret> Tank_Turret { get; set; } = new();

        public List<Engine> Engines { get; set; } = new();
    }
}
