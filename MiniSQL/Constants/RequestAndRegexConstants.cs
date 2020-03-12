using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Constants
{
    public class RequestAndRegexConstants
    {
        public static string queryGroup = "?<query>";
        public static string tableGroup = "?<table>";
        public static string selectedColumnGroup = "?<selectedColumn>";
        public static string toEvaluateColumnGroup = "?<toEvalColumn>";
        public static string valueGroup = "?<value>";
        public static string updatedColumnGroup = "?<updatedColumn>";
        public static string updatedvalueGroup = "?<toValue>";
        public static string operatorGroup = "?<operator>";
        public static string evalValue = "?<evalValue>";
        public static string columnGroup = "?<column>";
        public static string columnTypeGroup = "?<columnType>";
        public static string columnsTypes = "INT|DOUBLE|TEXT";
        public static string NAINCG = "[^\\* ,<=>\\(\\)]";
        public static string wherePatern = "WHERE (" + toEvaluateColumnGroup + NAINCG + "+)(" + operatorGroup + "[<=>])(" + evalValue + NAINCG + "+)";
        public static string fromPattern = "FROM (" + tableGroup + NAINCG + "+)(?: " + wherePatern + ")";
        public static string selectPattern = "^(" + queryGroup + "SELECT) (?:(\\*)|(" + selectedColumnGroup + NAINCG + "+)(?:,(" + selectedColumnGroup + NAINCG + "+))*) " + fromPattern + "?;$";
        public static string insertPattern = "^(" + queryGroup + "INSERT) INTO (" + tableGroup + NAINCG + "+)(?:\\((" + selectedColumnGroup + NAINCG + "+)(?:,(" + selectedColumnGroup + NAINCG + "+))*\\))? VALUES\\((" + valueGroup + NAINCG + "+)(?:,(" + valueGroup + NAINCG + "+))*\\);$";
        public static string dropPattern = "^(" + queryGroup + "DROP TABLE) (" + tableGroup + NAINCG + "+);$";
        public static string deletePattern = "^(" + queryGroup + "DELETE) " + fromPattern + "?;$";
        public static string updatePattern = "^(" + queryGroup + "UPDATE) (" + tableGroup + NAINCG + "+) SET (" + updatedColumnGroup + NAINCG + ")=(" + updatedvalueGroup + NAINCG + "+)(?:, (" + updatedColumnGroup + NAINCG + ")=(" + updatedvalueGroup + NAINCG + "+))* " + wherePatern + ";$";
        public static string createPattern = "^(" + queryGroup + "CREATE TABLE) (" + tableGroup + NAINCG + "+) \\((" + columnGroup + NAINCG + "+) (" + columnTypeGroup + columnsTypes + ")(?:, (" + columnGroup + NAINCG + "+) (" + columnTypeGroup + columnsTypes + "))*\\);$";


    }
}
