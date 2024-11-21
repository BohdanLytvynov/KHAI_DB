using Data.Interfaces;
using Data.Models.Accounts;
using Data.SqlScripts;
using Domain.DataControllers;
using Domain.Interfaces;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ViewModelBaseLibDotNetCore.Commands;
using ViewModelBaseLibDotNetCore.VM;
using CM = System.Configuration.ConfigurationManager;

namespace DB_Lab7.ViewModels.Pages
{
    public enum GetAllType : byte
    { 
        GetAllPressed = 1, 
        GetMyPressed
    }

    public enum Tank_SqlCommands : byte
    {
        GetAll = 1,
        Select,
        GetMyTanks,
        Search_All,
        Search_MY, 
        Sell_Tank
    }

    internal class MainPageViewModel : ViewModelBase
    {
        #region Delegates

        private Func<Account> m_getAccountDelegate;

        #endregion

        #region Fields

        private const string SQL_GET_ALL_TANKS = "SELECT t.Tank_Id as \"ID\", t.Name as \"Tank Name\", t.Strength, t.Armor, t.Max_Velocity, t.Velocity_rotate," +
            " t.Tank_Level,\r\ne.Name as \"Engine_Name\", e.Volume, e.Engine_Power, e.Fire_Probability, e.Max_Speed, e.Avg_Speed,\r\nn.Name as \"Nation Name\", " +
            "vc.Class_Name as \"Vehicle Class\", \r\ntt.Name as \"Tank_Turret_Name\", tt.View_range,tt.Velocity_Rotate ,\r\ntt.Armor, tt.Elevation_angle_Up ," +
            " tt.Elevation_angle_Down, tt.Strength, tt.View_radius,\r\nc.Name as \"Cannon Name\", c.DamagePerMinute, c.ReloadTime, c.FocusTime, c.Scatter," +
            "\r\np.Proj_Type, p.Damage, p.Armor_Penetration, p.Caliber" +
            "\r\nFROM TanksDb.Tank t " +
            "\r\nJOIN TanksDb.Nation n ON n.Nation_Id = t.Nation_Id " +
            "\r\nJOIN TanksDb.Vehicle_class vc ON vc.Vehicle_class_Id = t.Vehicle_Class_Id" +
            "\r\nJOIN TanksDb.Tank_Engine te ON te.Tank_Id = t.Tank_Id" +
            "\r\nJOIN TanksDb.Engine_ e ON e.Engine_Id = te.Engine_Id " +
            "\r\nJOIN TanksDb.Tank_Turret tt ON tt.Tank_Id = t.Tank_Id" +
            "\r\nJOIN TanksDb.Tank_Cannon tc ON tc.Tank_Turret_Id = tt.Tank_Turret_Id " +
            "\r\nJOIN TanksDb.Cannon c ON c.Cannon_Id = tc.Cannon_Id " +
            "\r\nJOIN TanksDb.Cannon_Projectile cp ON cp.Cannon_Id = tc.Cannon_Id" +
            "\r\nJOIN TanksDb.Projectile p ON p.Projectile_Id = cp.Projectile_Id;";

        private const string SQL_SELECT_TANK = "INSERT INTO TanksDb.Account_Tank (Account_Id, Tank_Id) VALUES (@AccountId, @TankId);";

        private const string SQL_GETTANKS_ACCORDING_TO_ACCOUNT_ID = "SELECT t.Tank_Id as \"ID\", a.Login as \"Owner\", t.Name as \"Tank Name\", t.Strength, " +
            "t.Armor, t.Max_Velocity, t.Velocity_rotate, t.Tank_Level,\r\ne.Name as \"Engine_Name\", e.Volume, e.Engine_Power, e.Fire_Probability, e.Max_Speed," +
            " e.Avg_Speed,\r\nn.Name as \"Nation Name\", vc.Class_Name as \"Vehicle Class\", \r\ntt.Name as \"Tank_Turret_Name\", tt.View_range,tt.Velocity_Rotate" +
            " ,\r\ntt.Armor, tt.Elevation_angle_Up , tt.Elevation_angle_Down, tt.Strength, tt.View_radius,\r\nc.Name as \"Cannon Name\", c.DamagePerMinute," +
            " c.ReloadTime, c.FocusTime, c.Scatter,\r\np.Proj_Type, p.Damage, p.Armor_Penetration, p.Caliber" +
            "\r\nFROM TanksDb.Tank t " +
            "\r\nJOIN TanksDb.Nation n ON n.Nation_Id = t.Nation_Id " +
            "\r\nJOIN TanksDb.Vehicle_class vc ON vc.Vehicle_class_Id = t.Vehicle_Class_Id" +
            "\r\nJOIN TanksDb.Tank_Engine te ON te.Tank_Id = t.Tank_Id" +
            "\r\nJOIN TanksDb.Engine_ e ON e.Engine_Id = te.Engine_Id " +
            "\r\nJOIN TanksDb.Tank_Turret tt ON tt.Tank_Id = t.Tank_Id" +
            "\r\nJOIN TanksDb.Tank_Cannon tc ON tc.Tank_Turret_Id = tt.Tank_Turret_Id " +
            "\r\nJOIN TanksDb.Cannon c ON c.Cannon_Id = tc.Cannon_Id " +
            "\r\nJOIN TanksDb.Cannon_Projectile cp ON cp.Cannon_Id = tc.Cannon_Id" +
            "\r\nJOIN TanksDb.Projectile p ON p.Projectile_Id = cp.Projectile_Id" +
            "\r\nJOIN TanksDb.Account_Tank at2 ON at2.Account_Id = @AccountId" +
            "\r\nJOIN TanksDb.Account a ON a.Account_Id = at2.Account_Id;";

        private const string SQL_SEARCH_ALL = "SELECT tank.Tank_Id as \"ID\", tank.Name as \"Tank Name\", tank.Strength, tank.Armor, tank.Max_Velocity, " +
            "tank.Velocity_rotate, tank.Tank_Level,\r\nengine_.Name as \"Engine_Name\", engine_.Volume, engine_.Engine_Power, engine_.Fire_Probability, " +
            "engine_.Max_Speed, engine_.Avg_Speed,\r\nnation.Name as \"Nation Name\", vc.Class_Name as \"Vehicle Class\", " +
            "\r\ntank_turret.Name as \"Tank_Turret_Name\", tank_turret.View_range,tank_turret.Velocity_Rotate ,\r\ntank_turret.Armor, " +
            "tank_turret.Elevation_angle_Up , tank_turret.Elevation_angle_Down, tank_turret.Strength, tank_turret.View_radius," +
            "\r\ncanon.Name as \"Cannon Name\", canon.DamagePerMinute, canon.ReloadTime, canon.FocusTime, canon.Scatter,\r\nprojectile.Proj_Type," +
            " projectile.Damage, projectile.Armor_Penetration, projectile.Caliber" +
            "\r\nFROM TanksDb.Tank tank " +
            "\r\nJOIN TanksDb.Nation nation ON nation.Nation_Id = tank.Nation_Id " +
            "\r\nJOIN TanksDb.Vehicle_class vc ON vc.Vehicle_class_Id = tank.Vehicle_Class_Id" +
            "\r\nJOIN TanksDb.Tank_Engine te ON te.Tank_Id = tank.Tank_Id" +
            "\r\nJOIN TanksDb.Engine_ engine_ ON engine_.Engine_Id = te.Engine_Id " +
            "\r\nJOIN TanksDb.Tank_Turret tank_turret ON tank_turret.Tank_Id = tank.Tank_Id" +
            "\r\nJOIN TanksDb.Tank_Cannon tc ON tc.Tank_Turret_Id = tank_turret.Tank_Turret_Id " +
            "\r\nJOIN TanksDb.Cannon canon ON canon.Cannon_Id = tc.Cannon_Id " +
            "\r\nJOIN TanksDb.Cannon_Projectile cp ON cp.Cannon_Id = tc.Cannon_Id" +
            "\r\nJOIN TanksDb.Projectile projectile ON projectile.Projectile_Id = cp.Projectile_Id" +
            "\r\nWHERE {0} = @value;";

        private const string SQL_SEARCH_MY = "SELECT tank.Tank_Id as \"ID\", a.Login as \"Owner\", tank.Name as \"Tank Name\", tank.Strength, tank.Armor, " +
            "tank.Max_Velocity, tank.Velocity_rotate, tank.Tank_Level,\r\nengine_.Name as \"Engine_Name\", engine_.Volume, engine_.Engine_Power, " +
            "engine_.Fire_Probability, engine_.Max_Speed, engine_.Avg_Speed,\r\nnation.Name as \"Nation Name\", vc.Class_Name as \"Vehicle Class\", " +
            "\r\ntank_turret.Name as \"Tank_Turret_Name\", tank_turret.View_range,tank_turret.Velocity_Rotate ,\r\ntank_turret.Armor, " +
            "tank_turret.Elevation_angle_Up , tank_turret.Elevation_angle_Down, tank_turret.Strength, tank_turret.View_radius,\r\ncanon.Name as \"Cannon Name\", " +
            "canon.DamagePerMinute, canon.ReloadTime, canon.FocusTime, canon.Scatter,\r\nprojectile.Proj_Type, projectile.Damage, projectile.Armor_Penetration, " +
            "projectile.Caliber" +
            "\r\nFROM TanksDb.Tank tank " +
            "\r\nJOIN TanksDb.Nation nation ON nation.Nation_Id = tank.Nation_Id " +
            "\r\nJOIN TanksDb.Vehicle_class vc ON vc.Vehicle_class_Id = tank.Vehicle_Class_Id" +
            "\r\nJOIN TanksDb.Tank_Engine te ON te.Tank_Id = tank.Tank_Id" +
            "\r\nJOIN TanksDb.Engine_ engine_ ON engine_.Engine_Id = te.Engine_Id " +
            "\r\nJOIN TanksDb.Tank_Turret tank_turret ON tank_turret.Tank_Id = tank.Tank_Id" +
            "\r\nJOIN TanksDb.Tank_Cannon tc ON tc.Tank_Turret_Id = tank_turret.Tank_Turret_Id " +
            "\r\nJOIN TanksDb.Cannon canon ON canon.Cannon_Id = tc.Cannon_Id " +
            "\r\nJOIN TanksDb.Cannon_Projectile cp ON cp.Cannon_Id = tc.Cannon_Id" +
            "\r\nJOIN TanksDb.Projectile projectile ON projectile.Projectile_Id = cp.Projectile_Id" +
            "\r\nJOIN TanksDb.Account_Tank at2 ON at2.Account_Id = @AccountId" +
            "\r\nJOIN TanksDb.Account a ON a.Account_Id = at2.Account_Id" +
            "\r\nWHERE {0} = @value;";

        private const string SQL_SELL_TANK = "DELETE FROm TanksDb.Account_Tank WHERE Account_Id = @AccountId AND Tank_Id = @TankId;";

        private string m_title;

        private string m_Inventory;

        private DataTable m_table;

        private DataController<Tank_SqlCommands> m_tanks_Controller;

        private int m_SelectedDataRowIndex;

        private string m_Search;

        private GetAllType m_GetAllType;

        private string m_toolTipSearch;

        private IDatabase m_database;

        private Visibility m_SellVisibility;

        #endregion

        #region Properties

        public DataTable Table { get => m_table; set => Set(ref m_table, value); }

        public string Title { get => m_title; set => Set(ref m_title, value); }

        public string Inventory { get => m_Inventory; set => Set(ref m_Inventory, value); }

        public int SelectedDataRowIndex { get => m_SelectedDataRowIndex; set => Set(ref m_SelectedDataRowIndex, value); }

        public string Search { get => m_Search; set => Set(ref m_Search, value); }

        public string ToolTipSearch { get => m_toolTipSearch; set => Set(ref m_toolTipSearch, value); }

        public Visibility SellButtonVisibility { get => m_SellVisibility; set => Set(ref m_SellVisibility, value); }

        #endregion

        #region Commands

        public ICommand OnGetTanksButtonPressed { get; }

        public ICommand OnSelectTankButtonPressed { get; }

        public ICommand OnGetMyTanksButtonPressed { get; }

        public ICommand OnSearchButtonPressed { get; }

        public ICommand OnSellButtonPressed { get; }

        #endregion

        #region IDataErrorInfo

        public override string this[string columnName]
        {
            get
            {
                var error = string.Empty;

                switch (columnName)
                {
                    case nameof(Search):
                        SetValidArray(0, Domain.Utilities.Validation.ValidateSearchField(Search, out error));
                        break;
                }

                return error;
            }
        }

        #endregion

        #region Ctor
        public MainPageViewModel(IDatabase database, Func<Account> getAccountDelegate)
        {
            #region Init Fields

            m_database = database;

            m_getAccountDelegate = getAccountDelegate;

            m_toolTipSearch = string.Empty;

            m_Search = string.Empty;

            m_title = "Lab7";

            m_Inventory = string.Empty;

            m_table = new();

            m_tanks_Controller = new(database);

            m_toolTipSearch = InitSearchToolTip();

            m_tanks_Controller.RegisterSqlScript(
                (Tank_SqlCommands.GetAll, SQL_GET_ALL_TANKS),
                (Tank_SqlCommands.Select, SQL_SELECT_TANK),
                (Tank_SqlCommands.GetMyTanks, SQL_GETTANKS_ACCORDING_TO_ACCOUNT_ID),
                (Tank_SqlCommands.Search_All, SQL_SEARCH_ALL),
                (Tank_SqlCommands.Search_MY, SQL_SEARCH_MY),
                (Tank_SqlCommands.Sell_Tank, SQL_SELL_TANK)
                );

            m_SelectedDataRowIndex = -1;

            InitValidArray(1);

            m_SellVisibility = Visibility.Collapsed;

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

            OnSearchButtonPressed = new Command(
                OnSearchButtonPressedExecute,
                CanOnSearchButtonPressedExecute
                );

            OnSellButtonPressed = new Command(
                OnSellButtonPressedExecute,
                CanOnSellButtonPressedExecute
                );

            #endregion
        }
        #endregion

        #region Functions

        #region On Get Tanks Button Pressed

        private bool CanOnGetTanksButtonPressedExecute(object p) => true;

        private void OnGetTanksButtonPressedExecute(object p)
        {
            m_GetAllType = GetAllType.GetAllPressed;

            var r = m_tanks_Controller.ExecuteQueryCommand(Tank_SqlCommands.GetAll);

            ParseResult(r, table => { Table = table; Inventory = "Store:"; });

            SelectedDataRowIndex = -1;

            SellButtonVisibility = Visibility.Collapsed;
        }

        #endregion

        #region On Select Tank Button Pressed

        private bool CanOnSelectTankButtonPressedExecute(object p) => m_SelectedDataRowIndex >= 0 && m_GetAllType == GetAllType.GetAllPressed;

        private void OnSelectTankButtonPressedExecute(object p)
        {
            var account = m_getAccountDelegate.Invoke();

            int tankId = (int)m_table.Rows[SelectedDataRowIndex].ItemArray[0];

            var r = m_tanks_Controller.ExecuteCommand(Tank_SqlCommands.Select, ("@AccountId", account.AccountId),
                ("@TankId", tankId));

            ParseResult(r, (rows) =>
            {
                if (rows == 0)
                    MessageBox.Show("Error Updating database!", Title, MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MessageBox.Show("Tank Selected", Title, MessageBoxButton.OK, MessageBoxImage.Information);

                    SelectedDataRowIndex = -1;
                }
                  
            });
        }

        #endregion

        #region On Get My Tanks Button Pressed

        private bool CanOnGetMyTanksButtonPressedExecute(object p) => true;

        private void OnGetMyTanksButtonPressedExecute(object p)
        {
            m_GetAllType = GetAllType.GetMyPressed;

            var a = m_getAccountDelegate.Invoke();

            var r = m_tanks_Controller.ExecuteQueryCommand(Tank_SqlCommands.GetMyTanks, ("@AccountId", a.AccountId));

            ParseResult(r, (table) => { Table = table; Inventory = "My Tanks:"; });

            SelectedDataRowIndex = -1;

            SellButtonVisibility = Visibility.Visible;
        }


        #endregion

        #region OnSearch Button Pressed

        private bool CanOnSearchButtonPressedExecute(object p)
        {
            return Validate(0, 0);
        }

        private void OnSearchButtonPressedExecute(object p)
        {
            IOperResult<DataTable> r = null;

            var trimed = Search.Trim(' ');
            var arr = trimed.Split(':');

            switch (m_GetAllType)
            {
                case GetAllType.GetAllPressed:
                    r = m_tanks_Controller.ExecuteQueryCommand(string.Format(SQL_SEARCH_ALL, arr[0]), ("@value", arr[1]));
                    break;
                case GetAllType.GetMyPressed:
                    var acc = m_getAccountDelegate.Invoke();
                    r = m_tanks_Controller.ExecuteQueryCommand(string.Format(SQL_SEARCH_MY, arr[0]),
                        ("@AccountId", acc.AccountId),
                        ("@value", arr[1]));
                    break;
            }

            ParseResult(r, (t) => Table = t);
        }

        #endregion

        #region On Sell Button Pressed

        private bool CanOnSellButtonPressedExecute(object p) => SelectedDataRowIndex >= 0 && m_GetAllType == GetAllType.GetMyPressed;

        private void OnSellButtonPressedExecute(object p)
        {
            var account = m_getAccountDelegate.Invoke();

            int tankId = (int)m_table.Rows[SelectedDataRowIndex].ItemArray[0];

            var r = m_tanks_Controller.ExecuteCommand(Tank_SqlCommands.Sell_Tank, ("@AccountId", account.AccountId),
                ("@TankId", tankId));

            ParseResult(r, (rows) =>
            {
                if (rows == 0)
                    MessageBox.Show("Error Updating database!", Title, MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MessageBox.Show("Tank Selected", Title, MessageBoxButton.OK, MessageBoxImage.Information);

                    SelectedDataRowIndex = -1;
                }

            });
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

        private string InitSearchToolTip()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("For search you can use this Pattern: <column_name : value> Here is the List of possible column names: \n");

            var tables = new List<string>();
            //Get All Tables for tool tips
            using (var con = m_database.Open())
            {               
                using (var com = m_database.BuildCommand(con, "SELECT TableName FROM TanksDb.TablesForToolTips;"))
                {
                    try
                    {
                        using (var r = com.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                tables.Add(r.GetString(0));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Error on analyzing database! Fail to create a ToolTip for Search Field!" +
                            $" Msg: {e.Message}", Title, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            foreach (var table in tables)
            {
                using (var con = m_database.Open())
                {
                    using (var com = m_database.BuildCommand(con, $"DESCRIBE {table}"))
                    {
                        try
                        {
                            using (var r = com.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    sb.Append(string.Concat(table.ToLower(), ".", r.GetString(0), ", ", "\t\n"));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show($"Error on analyzing database! Fail to create a ToolTip for Search Field!" +
                                $" Msg: {e.Message}", Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }

            return sb.ToString();

        }

        #endregion
    }
}
