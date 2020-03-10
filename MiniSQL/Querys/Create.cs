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
                    this.SetResult(this.GetResult() + QuerysStringResultConstants.TheTableAlreadyExists(this.targetTableName) + "\n");
                    this.IncrementErrorCount();
                }              
            }
            else 
            {
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
            this.GetContainer().GetDatabase(this.targetDatabase).AddTable(newTable);
            this.SetResult(this.GetResult() + QuerysStringResultConstants.TableWasCreated(this.targetDatabase, this.targetTableName) + "\n");
        }

        public void AddColumn(string columnName, string dataTypeKey) 
        {
            if (!this.columnsAndTypes.ContainsKey(columnName)) 
            {
                this.columnsAndTypes.Add(columnName, dataTypeKey);
            }
            else 
            {
                this.SetResult(this.GetResult() + QuerysStringResultConstants.TheColumnAlreadyDefined(columnName) + "\n");
                this.IncrementErrorCount();
            }
        }

    }
}
