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
        public static string CreateGroupDependingXML(Match match) 
        {
            return null;        
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
