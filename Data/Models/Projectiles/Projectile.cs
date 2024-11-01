using Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Projectiles
{
    public class Projectile : DataBaseEntity<int>
    {        
        public string Projectile_Type { get; set; } = string.Empty;

        public float Damage { get; set; }

        public float Armor_Penetration { get; set; }

        public float Caliber { get; set; }
    }
}
