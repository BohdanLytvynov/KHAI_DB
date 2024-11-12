using DB_Lab7.ViewModels.DataViewModels.Base;
using System.Collections.ObjectModel;

namespace DB_Lab7.ViewModels.DataViewModels
{
    internal class Cannon_ViewModel : DataViewModelBase<int>
    {
        #region Fields
        private string m_Name;
        private float m_DPM;
        private float m_ReloadTime;
        private float m_FocusTime;
        private float m_Scatter;
        private ObservableCollection<Projectile_ViewModel> m_Projectiles;
        #endregion

        #region Properties

        public string Name { get => m_Name; set => Set(ref m_Name, value); }

        public float DPM { get => m_DPM; set => Set(ref m_DPM, value); }

        public float ReloadTime { get => m_ReloadTime; set => Set(ref m_ReloadTime, value); }

        public float FocusTime { get => m_FocusTime; set => Set(ref m_FocusTime, value); }

        public float Scatter { get => m_Scatter; set => Set(ref m_Scatter, value); }

        public ObservableCollection<Projectile_ViewModel> Projectiles 
        { get => m_Projectiles; set => m_Projectiles = value; }

        #endregion

        #region Ctor
        public Cannon_ViewModel(int id,
            string name,
            float dpm,
            float reloadTime,
            float focusTime,
            float scatter,
            IEnumerable<Projectile_ViewModel> projectiles) 
            : base(id)
        {
            Name = name;
            DPM = dpm;
            ReloadTime = reloadTime;
            FocusTime = focusTime;
            Scatter = scatter;

            Projectiles = new();

            if (projectiles is not null && projectiles.Count() > 0)
            {
                foreach (var item in projectiles)
                { 
                    Projectiles.Add(item);
                }
            }
        }
        #endregion
    }
}
