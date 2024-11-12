using Data.Models.Base;
using Data.Models.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Cannons
{
    public class Cannon : DataBaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;

        public float DPM { get; set; }

        public float ReloadTime { get; set; }

        public float FocusTime { get; set; }

        public float Scatter { get; set; }

        public IEnumerable<Projectile> Projectiles { get; set; }
    }
}
