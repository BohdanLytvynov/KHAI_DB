using DB_Lab7.ViewModels.DataViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Lab7.ViewModels.DataViewModels
{
    internal class Engine_ViewModel : DataViewModelBase<int>
    {
        #region Fields
        private string m_Name;
        private float m_Volume;
        private float m_Engine_Power;
        private float m_Fire_Probability;
        private float m_Max_Speed;
        private float m_Avg_Speed;
        #endregion

        #region Properties

        public string Name { get => m_Name; set => Set(ref m_Name, value); }

        public float Volume { get => m_Volume; set => Set(ref m_Volume, value); }

        public float Engine_Power { get => m_Engine_Power; set => Set(ref m_Engine_Power, value); }

        public float Fire_Probability { get => m_Fire_Probability; set => Set(ref m_Fire_Probability, value); }

        public float Max_Speed { get => m_Fire_Probability; set => Set(ref m_Fire_Probability, value); }

        public float Avg_Speed { get => m_Avg_Speed; set => Set(ref m_Avg_Speed, value); }

        #endregion

        #region Ctor
        public Engine_ViewModel(int id) : base(id)
        {
                
        }
        #endregion
    }
}
