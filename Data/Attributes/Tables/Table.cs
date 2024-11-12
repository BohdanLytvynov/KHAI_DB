namespace Data.Attributes.Tables
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Table : Attribute
    {
        #region Properties

        public string TableName { get; }

        #endregion

        #region Ctor
        public Table(string tableName)
        {
            TableName = tableName;
        }
        #endregion
    }
}
