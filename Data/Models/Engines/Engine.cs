using Data.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.Engines
{
    [Table("Engine_")]
    public class Engine : DataBaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;

        public float Volume { get; set; }

        public float Engine_Power { get; set; }

        public float Fire_Probability { get; set; }

        public float Max_Speed { get; set; }

        public float Avg_Speed { get; set; }
    }
}
