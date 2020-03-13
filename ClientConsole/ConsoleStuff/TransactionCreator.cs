using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientConsole.ConsoleStuff
{
    public class TransactionCreator
    {
        private static TransactionCreator transactionCreator;

        private TransactionCreator() 
        { 
        
        }

        public static TransactionCreator GetTransactionCreator() 
        {
            if (transactionCreator == null) transactionCreator = new TransactionCreator();
            return transactionCreator;
        }

        public string CreateGroupDependingXML(Match match) 
        {
            string xmlString = "<transaction>";
            xmlString = xmlString + "\n <fullQuery>" + match.Groups[0] + "</fullQuery>";
            for (int i = 1; i < match.Groups.Count; i++) 
            {
                if(!match.Groups[i].Value.Equals("")) xmlString = xmlString + "\n <" + match.Groups[i].Name + ">" + match.Groups[i].Value + "</" + match.Groups[i].Name + ">";               
            }
            xmlString = xmlString + "\n</transaction>";
            return xmlString;        
        }

        /**
         * This is common structure for all of queryes
         *<Transaction>
         *  <QueryData>
         *      <TypeOfQuery>SELECT/UPDATE/INSERT/DELETE/CREATE/DROP/</TypeOfQuery>
         *      <DateOfQuery></DateOfQuery>   
         *      <Database></Database>
         *  </QueryData>
         * </Transaction> 
         * 
         **/

        /**
         * Especificaly to Select with the 
         * <Transaction>
         *  <QueryData>
         *      <TypeOfQuery>SELECT/UPDATE/DELETE/UPDATE</TypeOfQuery>
         *      <DateOfQuery></DateOfQuery>
         *      <Table>**Tablename**</Table>
         *      <SelectedColumns> (if they put *, then only one <ColumnName></ColumnName>) with the *
         *          <ColumnName></ColumnName>
         *          <ColumnName></ColumnName>
         *          <ColumnName></ColumnName>
         *      </SelectedColumns>
         *      <Where>
         *          <Clause>
         *              <ColumnName></ColumnName>
         *              <Operator></Operator>
         *              <Value></Value>
         *          </Clause>
         *      </Where>
         *  </QueryData>
         * </Transaction> 
         * 
         * 
         **/

        /**
         * Especificaly to Update
         * <Transaction>
         *  <QueryData>
         *      <TypeOfQuery>SELECT/UPDATE/DELETE/UPDATE</TypeOfQuery>
         *      <DateOfQuery></DateOfQuery>
         *      <Table>**Tablename**</Table>
         *      <ColumnsToUpdate>
         *          <Update>
         *              <ColumnName></ColumnName>
         *              <Value></Value>
         *          </Update>
         *          <Update>
         *              <ColumnName></ColumnName>
         *              <Value></Value>
         *          </Update>
         *          <Update>
         *              <ColumnName></ColumnName>
         *              <Value></Value>
         *          </Update>
         *      </ColumnsToUpdate>
         *      <Where>
         *          <Clause>
         *              <ColumnName></ColumnName>
         *              <Operator></Operator>
         *              <Value></Value>
         *          </Clause>
         *      </Where>
         *  </QueryData>
         * </Transaction> 
         * 
         * Especificaly to Delete
         * 
         * 
         * 
         **/






    }
}
