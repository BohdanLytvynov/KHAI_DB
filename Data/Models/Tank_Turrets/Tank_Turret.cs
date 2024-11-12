using Data.Models.Base;
using Data.Models.Cannons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Tank_Turrets
{
    [Table("Tank_Turret")]
    public class Tank_Turret : DataBaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;

        public float View_range { get; set; }

        public float View_radius { get; set; }

        public float Velocity_Rotate { get; set; }

        public float Armor { get; set; }

        public float Elevation_angle_Up { get; set; }

        public float Elevation_angle_Down { get; set; }

        public float Strength { get; set; }

        public IEnumerable<Cannon> Cannons { get; set; }

    }
}
