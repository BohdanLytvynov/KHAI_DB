using Data.Models.Cannons;
using DB_Lab7.ViewModels.DataViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lab7.ViewModels.DataViewModels
{
    internal class TankTurret_ViewModel : DataViewModelBase<int>
    {
        #region Fields
        private string m_Name;
        private float m_View_range;
        private float m_View_radius;
        private float m_Velocity_Rotate;
        private float m_Armor;
        private float m_Elevation_angle_Up;
        private float m_Elevation_angle_Down;
        private float m_Strength;
        private ObservableCollection<Cannon_ViewModel> m_Cannons;

        #endregion

        #region Properties

        public string Name { get => m_Name; set => Set(ref m_Name, value); }

        public float View_range { get => m_View_range; set => Set(ref m_View_range, value); }

        public float View_radius { get => m_View_radius; set => Set(ref m_View_radius, value); }

        public float Velocity_Rotate { get => m_Velocity_Rotate; set => Set(ref m_Velocity_Rotate, value); }

        public float Armor { get => m_Armor; set => Set(ref m_Armor, value); }

        public float Elevation_angle_Up { get => m_Elevation_angle_Up; set => Set(ref m_Elevation_angle_Up, value); }

        public float Elevation_angle_Down { get => m_Elevation_angle_Down; set => Set(ref m_Elevation_angle_Down, value); }

        public float Strength { get => m_Strength; set => Set(ref m_Strength, value); }

        public ObservableCollection<Cannon_ViewModel> Cannons 
        { get => m_Cannons; set => m_Cannons = value; }

        #endregion

        #region Ctor
        public TankTurret_ViewModel(int id,
            string name,
            float viewRange,
            float viewRadius,
            float velocityRotate,
            float armor,
            float elevationAngleUp,
            float elevationAngleDown,
            float strength,
            IEnumerable<Cannon_ViewModel> cannons) : base(id)
        {
            Name = name;
            View_range = viewRange;
            View_radius = viewRadius;
            Velocity_Rotate = velocityRotate;
            Armor = armor;
            Elevation_angle_Down = elevationAngleDown;
            Elevation_angle_Up = elevationAngleUp;
            Strength = strength;

            Cannons = new();

            if(cannons is not null && cannons.Count() > 0)
                foreach (var item in cannons)
                {
                    Cannons.Add(item);
                }
        }

        public TankTurret_ViewModel(int id) : base(id) 
        {
            
        }
        #endregion
    }
}
