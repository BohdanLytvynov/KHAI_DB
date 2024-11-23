using Data.Interfaces;
using Domain.Interfaces;
using System.Data;

namespace Data.DataControllers
{
    public enum GetAllType : byte
    {
        GetAllPressed = 1,
        GetMyPressed
    }

    public enum Tank_SqlCommands : byte
    {
        GetAll = 1,
        Buy_Tank,
        GetMyTanks,
        Search_All,
        Search_MY,
        Sell_Tank
    }

    public class TankController : DataController<Tank_SqlCommands>, ICRUDController
    {
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
                
        private const string SQL_SELL_TANK = "DELETE FROm TanksDb.Account_Tank WHERE Account_Id = @AccountId AND Tank_Id = @TankId;";

        #endregion

        #region Properties

        #endregion

        #region Ctor

        public TankController(IDatabase db) : base(db)
        {
            #region Register SQL Scripts

            RegisterSqlScript(
                (Tank_SqlCommands.GetAll, SQL_GET_ALL_TANKS),
                (Tank_SqlCommands.Buy_Tank, SQL_SELECT_TANK),
                (Tank_SqlCommands.GetMyTanks, SQL_GETTANKS_ACCORDING_TO_ACCOUNT_ID),
                (Tank_SqlCommands.Search_All, SQL_SEARCH_ALL),                
                (Tank_SqlCommands.Sell_Tank, SQL_SELL_TANK)
                );

            #endregion
        }

        #endregion

        #region Methods

        public DataTable GetAll()
        { 
            return ExecuteQueryCommand(Tank_SqlCommands.GetAll);
        }

        DataTable ICRUDController.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DataTable GetTankByAccountId(int AccountId)
        {
            return ExecuteQueryCommand(Tank_SqlCommands.GetMyTanks, ("@AccountId", AccountId));
        }

        public int BuyTank(int AccountId, int tankId)
        {
            return ExecuteCommand(Tank_SqlCommands.Buy_Tank, ("@AccountId", AccountId),
                ("@TankId", tankId));
        }

        public DataTable Search(string condition, string value)
        { 
            return ExecuteQueryCommand(string.Format(SQL_SEARCH_ALL, condition), ("@value", value));
        }
          
        public int Edit()
        {
            throw new NotImplementedException();
        }

        public int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
