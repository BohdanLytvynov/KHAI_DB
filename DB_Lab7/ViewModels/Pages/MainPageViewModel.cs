using Data.Interfaces;
using Data.Models.Accounts;
using Data.SqlScripts;
using Domain.DataControllers;
using Domain.Interfaces;
using System.Data;
using System.Windows;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;

namespace DB_Lab7.ViewModels.Pages
{
    public enum Tank_SqlCommands : byte
    {
        GetAll = 1,
        Select,
        GetMyTanks
        
    }

    internal class MainPageViewModel : ViewModelBase
    {
        #region Delegates

        private Func<Account> m_getAccountDelegate;

        #endregion

        #region Fields

        private const string SQL_GET_ALL_TANKS = "SELECT t.Tank_Id as \"ID\", t.Name as \"Tank Name\", t.Strength, t.Armor, t.Max_Velocity, t.Velocity_rotate, t.Tank_Level,\r\ne.Name as \"Engine_Name\", e.Volume, e.Engine_Power, e.Fire_Probability, e.Max_Speed, e.Avg_Speed,\r\nn.Name as \"Nation Name\", vc.Class_Name as \"Vehicle Class\", \r\ntt.Name as \"Tank_Turret_Name\", tt.View_range,tt.Velocity_Rotate ,\r\ntt.Armor, tt.Elevation_angle_Up , tt.Elevation_angle_Down, tt.Strength, tt.View_radius,\r\nc.Name as \"Cannon Name\", c.DamagePerMinute, c.ReloadTime, c.FocusTime, c.Scatter,\r\np.Proj_Type, p.Damage, p.Armor_Penetration, p.Caliber\r\nFROM TanksDb.Tank t \r\nJOIN TanksDb.Nation n ON n.Nation_Id = t.Nation_Id \r\nJOIN TanksDb.Vehicle_class vc ON vc.Vehicle_class_Id = t.Vehicle_Class_Id\r\nJOIN TanksDb.Tank_Engine te ON te.Tank_Id = t.Tank_Id\r\nJOIN TanksDb.Engine_ e ON e.Engine_Id = te.Engine_Id \r\nJOIN TanksDb.Tank_Turret tt ON tt.Tank_Id = t.Tank_Id\r\nJOIN TanksDb.Tank_Cannon tc ON tc.Tank_Turret_Id = tt.Tank_Turret_Id \r\nJOIN TanksDb.Cannon c ON c.Cannon_Id = tc.Cannon_Id \r\nJOIN TanksDb.Cannon_Projectile cp ON cp.Cannon_Id = tc.Cannon_Id\r\nJOIN TanksDb.Projectile p ON p.Projectile_Id = cp.Projectile_Id;";

        private const string SQL_SELECT_TANK = "INSERT INTO TanksDb.Account_Tank (Account_Id, Tank_Id) VALUES (@AccountId, @TankId);";

        private const string SQL_GETTANKS_ACCORDING_TO_ACCOUNT_ID = "SELECT t.Tank_Id as \"ID\", a.Login as \"Owner\", t.Name as \"Tank Name\", t.Strength, t.Armor, t.Max_Velocity, t.Velocity_rotate, t.Tank_Level,\r\ne.Name as \"Engine_Name\", e.Volume, e.Engine_Power, e.Fire_Probability, e.Max_Speed, e.Avg_Speed,\r\nn.Name as \"Nation Name\", vc.Class_Name as \"Vehicle Class\", \r\ntt.Name as \"Tank_Turret_Name\", tt.View_range,tt.Velocity_Rotate ,\r\ntt.Armor, tt.Elevation_angle_Up , tt.Elevation_angle_Down, tt.Strength, tt.View_radius,\r\nc.Name as \"Cannon Name\", c.DamagePerMinute, c.ReloadTime, c.FocusTime, c.Scatter,\r\np.Proj_Type, p.Damage, p.Armor_Penetration, p.Caliber\r\nFROM TanksDb.Tank t \r\nJOIN TanksDb.Nation n ON n.Nation_Id = t.Nation_Id \r\nJOIN TanksDb.Vehicle_class vc ON vc.Vehicle_class_Id = t.Vehicle_Class_Id\r\nJOIN TanksDb.Tank_Engine te ON te.Tank_Id = t.Tank_Id\r\nJOIN TanksDb.Engine_ e ON e.Engine_Id = te.Engine_Id \r\nJOIN TanksDb.Tank_Turret tt ON tt.Tank_Id = t.Tank_Id\r\nJOIN TanksDb.Tank_Cannon tc ON tc.Tank_Turret_Id = tt.Tank_Turret_Id \r\nJOIN TanksDb.Cannon c ON c.Cannon_Id = tc.Cannon_Id \r\nJOIN TanksDb.Cannon_Projectile cp ON cp.Cannon_Id = tc.Cannon_Id\r\nJOIN TanksDb.Projectile p ON p.Projectile_Id = cp.Projectile_Id\r\nJOIN TanksDb.Account_Tank at2 ON at2.Account_Id = @AccountId\r\nJOIN TanksDb.Account a ON a.Account_Id = at2.Account_Id;";

        private string m_title;

        private DataTable m_table;

        private DataController<Tank_SqlCommands> m_tanks_Controller;
      
        private int m_SelectedDataRowIndex;

        #endregion

        #region Properties

        public DataTable Table { get => m_table; set => Set(ref m_table, value); }

        public string Title { get => m_title; set => Set(ref m_title, value); }

        public int SelectedDataRowIndex { get => m_SelectedDataRowIndex; set => Set(ref m_SelectedDataRowIndex, value); }

        #endregion

        #region Commands

        public ICommand OnGetTanksButtonPressed { get;}

        public ICommand OnSelectTankButtonPressed { get; }

        public ICommand OnGetMyTanksButtonPressed { get; }

        #endregion

        #region Ctor
        public MainPageViewModel(IDatabase database, Func<Account> getAccountDelegate)
        {
            #region Init Fields

            m_getAccountDelegate = getAccountDelegate;

            m_title = "Lab7";

            m_table = new();

            m_tanks_Controller = new(database);

            m_tanks_Controller.RegisterSqlScript(
                (Tank_SqlCommands.GetAll, SQL_GET_ALL_TANKS),
                (Tank_SqlCommands.Select, SQL_SELECT_TANK),
                (Tank_SqlCommands.GetMyTanks, SQL_GETTANKS_ACCORDING_TO_ACCOUNT_ID)
                );
            
            m_SelectedDataRowIndex = -1;

            #endregion

            #region Init Commands

            OnGetTanksButtonPressed = new Command(
                OnGetTanksButtonPressedExecute,
                CanOnGetTanksButtonPressedExecute
                );

            OnSelectTankButtonPressed = new Command(
                OnSelectTankButtonPressedExecute,
                CanOnSelectTankButtonPressedExecute
                );

            OnGetMyTanksButtonPressed = new Command(
                OnGetMyTanksButtonPressedExecute,
                CanOnGetMyTanksButtonPressedExecute
                );

            #endregion
        }
        #endregion

        #region Functions

        #region On Get Tanks Button Pressed

        private bool CanOnGetTanksButtonPressedExecute(object p) => true;

        private void OnGetTanksButtonPressedExecute(object p)
        {
            var r = m_tanks_Controller.ExecuteQueryCommand(Tank_SqlCommands.GetAll);

            ParseResult(r, table => Table = table);
        }

        #endregion

        #region On Select Tank Button Pressed

        private bool CanOnSelectTankButtonPressedExecute(object p) => m_SelectedDataRowIndex >= 0;

        private void OnSelectTankButtonPressedExecute(object p)
        {
            var account = m_getAccountDelegate.Invoke();

            int tankId = (int)m_table.Rows[SelectedDataRowIndex].ItemArray[0];

            var r = m_tanks_Controller.ExecuteCommand(Tank_SqlCommands.Select, ("@AccountId", account.AccountId),
                ("@TankId", tankId));

            ParseResult(r, (rows) => 
            {
                if(rows == 0)
                    MessageBox.Show("Error Updating database!", Title, MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show("Tank Selected", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        #endregion

        #region On Get My Tanks Button Pressed

        private bool CanOnGetMyTanksButtonPressedExecute(object p) => true;

        private void OnGetMyTanksButtonPressedExecute(object p)
        {
            var a = m_getAccountDelegate.Invoke();

            var r = m_tanks_Controller.ExecuteQueryCommand(Tank_SqlCommands.GetMyTanks, ("@AccountId", a.AccountId));

            ParseResult(r, (table) => Table = table);
        }


        #endregion

        private void ParseResult<TResult>(IOperResult<TResult> result, Action<TResult> execOnSuccess = default)
        {
            if (result.HasError)
            {
                MessageBox.Show(result.Error.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                execOnSuccess?.Invoke(result.Result);                
            }
        }

        #endregion
    }
}
