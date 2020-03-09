using MiniSQL.Classes;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Update : DataChangeQuery
    {
        
        public Update(IDatabaseContainer container) : base(container)
        {
            
        }
        /**
         * Despues de hoy me he dado cuenta (o recordado) que el insert y el update comparten cosas.
         * La una coleccion en la cual se almacenan los nombres de las columnas y los datos a insertar
         * modificar y la validacion de parametros.
         * Por ello he creado una clase abstracta (otra mas) de la que heredan update e insert (otra herencia
         * mas en la hierarquia de las queris)
         * En esta clase, hay una coleccion de List<Tuple<string, string>> afectedColumnsAndValues, y un metodo publico
         * para añadir desde fuera los nombres de las columnas con sus datos que se quieren updatear.
         * Para conseguir un enumerador de esa lista se hace con this.GetColumnAndDataEnumerator(); 
         * 
         * Tambien me he dado cuenta, que para obtener los objetos de tipo Row, se puede usar la query select aqui. La
         * forma de usarla seria asi:
         * Select select = new Select(this.GetContainer);
         * select.where = this.where;
         * select.selectedAllColumns = true;
         * select.ExecuteParticularQueryAction(Table table) (con la table que se le pasa)
         * Una vez hecho esto, el objeto select almacena una coleccion de Row que es el resultado de las filas obtenidas 
         * por la consulta, se puede obtener un enumerado mediante select.GetSelectedRows();
         * 
         * 
         * Una vez hecho eso, se recorre el enumerador en un while, y dentro de ese while se obtiene otro enumerador de
         * afectedColumnsAndValues y se hace otro while modificando las cells. Comente el codigo para que no de errores.
         * 
         * 
         **/
        public override void ExecuteParticularQueryAction(Table table)
        {
            
           /** IEnumerator<Row> rowEnumerator = table.GetRowEnumerator();
            while (rowEnumerator.MoveNext())
            {
                if (this.whereClause.IsSelected(rowEnumerator.Current))
                {
                    IEnumerator<KeyValuePair<string, string>> rowColum = updateColumnData.GetEnumerator();
                    while(rowColum.MoveNext())
                    {
                        rowEnumerator.Current.GetCell(rowColum.Current.Key).data=rowColum.Current.Value;
                    }
                }
            }  
            */

        }
    }
}
