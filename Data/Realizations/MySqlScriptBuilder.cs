using Data.Attributes.Bindings;
using Data.Attributes.Columns;
using Data.Attributes.Tables;
using Data.Database;
using Data.Exceptions;
using Data.Interfaces;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Realizations
{
    public class MySqlScriptBuilder : ISqlScriptBuilder
    {
        private const string SPACE = " ";
        private const string COMMA = ",";
        private const string DOT = ".";

        public string BuildScript(Type entity, IEnumerable<Type> models, ScriptAction action)
        {            
            switch (action)
            {
                case ScriptAction.Create:
                    return BuildReadScript(entity, models);
                    
                case ScriptAction.Read:

                    break;
                case ScriptAction.Update:

                    break;
                case ScriptAction.Delete:

                    break;            
            }

            return null;
        }

        private string BuildReadScript(Type entity, IEnumerable<Type> models)
        {
            string tableName = string.Empty;

            string columnName = string.Empty;
                        
            string alias = tableName.ToLower();

            List<PropertyInfo> oneToMany = new();
            List<PropertyInfo> manyToMany = new();

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT");
            
            var entityProps = entity.GetProperties();

            foreach (var prop in entityProps)
            {
                //We have found the property with Binding
                if (prop.PropertyType.BaseType.Name.Equals("DataBaseEntity"))
                {
                    if (prop.GetCustomAttribute<OneToMany>() is not null)                    
                        oneToMany.Add(prop);       
                    
                    else if (prop.GetCustomAttribute<ManyToMany>() is not null)                    
                        manyToMany.Add(prop);   
                    
                    else if (prop.GetCustomAttribute<OneToMany>() is not null &&
                        prop.GetCustomAttribute<ManyToMany>() is not null)                    
                        throw new DataModelsBindingError($"It seems you are using 'OneToMany'" +
                            $" and 'ManyToMany' Attribute on Property {prop.Name}! " +
                            $"You should use only one Binding Type!"); 
                    
                    else if (prop.GetCustomAttribute<OneToMany>() is null
                        || prop.GetCustomAttribute<ManyToMany>() is null)                    
                        throw new DataModelsBindingError("You are using the Complex Model type! " +
                            "But you haven't specified the Binding Type!");                    
                }
                else//Ordinary Property 
                {                    
                    if (prop.GetCustomAttribute<Column>() is not null)                    
                        columnName = prop.GetCustomAttribute<Column>().ColumnName;                    
                    else
                        columnName = prop.Name;
                }

                sb.Append(SPACE)
                    .Append(alias)
                    .Append(DOT)
                    .Append(columnName)
                    .Append(COMMA);                 
            }

            sb.Append(SPACE).Append("FROM");
            sb.Append(SPACE).Append(tableName)
                .Append(SPACE)
                .Append("AS")
                .Append(SPACE)                
                .Append(alias);

            //Need to add Some Joins
            if (oneToMany.Count > 0)
            { 
                
            }

            return sb.ToString();
        }
    }
}
