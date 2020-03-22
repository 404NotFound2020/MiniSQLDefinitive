using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.DataTypes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Create : DataDefinitionQuery
    {
        //Column name, datatype
        private Dictionary<string, string> columnsAndTypes;

        public Create(IDatabaseContainer container) : base(container)
        {
            this.columnsAndTypes = new Dictionary<string, string>();
        }

        public override bool ValidateParameters()
        {
            if (this.GetContainer().ExistDatabase(this.targetDatabase)) 
            {
                Database database = this.GetContainer().GetDatabase(this.targetDatabase);
                if (database.ExistTable(this.targetTableName)) 
                { 
                    this.SetResult(QuerysStringResultConstants.TheTableAlreadyExists(this.targetTableName));
                    this.IncrementErrorCount();
                }              
            }
            else 
            {
                this.SetResult(QuerysStringResultConstants.DatabaseDoesntExist(this.targetDatabase));
                this.IncrementErrorCount();
            }
            return this.GetIsValidQuery();
        }


        public override void ExecuteParticularQueryAction()
        {
            Table newTable = new Table(this.targetTableName);
            IEnumerator<KeyValuePair<string, string>> enumerator = columnsAndTypes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                newTable.AddColumn(new Column(enumerator.Current.Key, DataTypesFactory.GetDataTypesFactory().GetDataType(enumerator.Current.Value)));
            }
            Database afectedDatabase = this.GetContainer().GetDatabase(this.targetDatabase);
            afectedDatabase.AddTable(newTable);
            this.GetContainer().SaveTable(afectedDatabase, newTable);
            this.SetResult(QuerysStringResultConstants.TableWasCreated(this.targetDatabase, this.targetTableName));
        }

        public void AddColumn(string columnName, string dataTypeKey) 
        {
            if (!this.columnsAndTypes.ContainsKey(columnName)) 
            {
                this.columnsAndTypes.Add(columnName, dataTypeKey);
            }
            else 
            {
                this.SetResult(QuerysStringResultConstants.TheColumnAlreadyDefined(columnName));
                this.IncrementErrorCount();
            }
        }

    }
}
