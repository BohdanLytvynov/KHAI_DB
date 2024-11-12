using Data.Models.Projectiles;
using DB_Lab7.ViewModels.DataViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;

namespace DB_Lab7.ViewModels.DataViewModels
{
    internal class Projectile_ViewModel : DataViewModelBase<int>
    {
        #region Fields

        private string m_Projectile_Type;
        private float m_Damage;
        private float m_Armor_Penetration;
        private float m_Caliber;

        #endregion

        #region Properties
        public string Projectile_Type { get => m_Projectile_Type; set => Set(ref m_Projectile_Type, value); }

        public float Damage { get => m_Damage; set => Set(ref m_Damage, value); }

        public float Armor_Penetration { get=> m_Armor_Penetration; set => Set(ref m_Armor_Penetration, value); }

        public float Caliber { get => m_Caliber; set => Set(ref m_Caliber, value); }
        #endregion

        #region Ctor
        public Projectile_ViewModel(int id, string prjType, float damage, float armorPenetration, float caliber)
            :  base(id)
        {
            Id = id;
            Projectile_Type = prjType;
            Damage = damage;
            Armor_Penetration = armorPenetration;
            Caliber = caliber;
        }

        public Projectile_ViewModel(int id) : base(id)
        {
            
        }
        #endregion


    }
}
