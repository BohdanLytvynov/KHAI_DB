using Data.Models.Engines;
using Data.Models.Nations;
using Data.Models.Tank_Turrets;
using DB_Lab7.ViewModels.DataViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lab7.ViewModels.DataViewModels
{
    internal class Tank_ViewModel : DataViewModelBase<int>
    {
        #region Fields

        private string m_Name;
        private float m_Strength;
        private float m_Armor;
        private float m_Max_Velocity;
        private float m_Velocity_rotate;
        private uint m_Tank_Level;
        private string m_Nation;
        private ObservableCollection<Tank_Turret> m_Tank_Turret;
        private ObservableCollection<Engine> m_Engines;

        #endregion

        #region Properties

        public string Name { get => m_Name; set => Set(ref m_Name, value); }

        public float Strength { get => m_Strength; set => Set(ref m_Strength, value); }

        public float Armor { get => m_Armor; set => Set(ref m_Armor, value); }

        public float Max_Velocity { get => m_Max_Velocity; set => Set(ref m_Max_Velocity, value); }

        public float Velocity_rotate { get => m_Velocity_rotate; set => Set(ref m_Velocity_rotate, value); }

        public uint Tank_Level { get => m_Tank_Level; set => Set(ref m_Tank_Level, value); }

        public string Nation { get => m_Nation; set => Set(ref m_Nation, value); }

        public ObservableCollection<Tank_Turret> Tank_Turret 
        { get => m_Tank_Turret; set => m_Tank_Turret = value; }

        public ObservableCollection<Engine> Engines 
        { get => m_Engines; set => m_Engines = value; }

        #endregion

        #region Ctor
        public Tank_ViewModel(int id) : base(id)
        {
            
        }
        #endregion
    }
}
