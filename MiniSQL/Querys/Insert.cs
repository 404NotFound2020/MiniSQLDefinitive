using MiniSQL.Classes;
using MiniSQL.Constants;
using MiniSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQL.Querys
{
    public class Insert : DataChangeQuery
    {

        public Insert(IDatabaseContainer container) : base(container)
        {

        }

        /**
         * Los nombres de las columnas y los datos que se quieren almacenar en ellas estan en la coleccion List<Tuple<string, string>> afectedColumnsAndValues de la clase
         * DataChangeQuery (en el paquete interfaces)
         * 
         * Me he dado cuenta, que la validacion, tanto para update como para insert, es la misma, y que ambas necesitan guardar datos de las columnas en las que se quiere
         * insertar/modificar datos, es por ello, que he creado la clase esa de DataChangeQuery, en la cual estan los metodos de validar parametros y añadir los nombres
         * de las columnas con los datos a meter.
         * 
         * Realmente, tal y como lo tenemos ahora mismo, la unica diferencia entre update e insert es que en update no se crea nueva fila.
         * 
         * Lo que se debe de hacer en la insert es
         * 1. Crear nueva fila con lo de create row definition
         * 2. Obtener un enumerador de tuplas de afectedColumnsAndValues. Cada tupla tiene dos elementos (mister obvius), el primer elemento representa el nombre de la columna
         * mientras que el segundo representa el dato a insertar, estos se obtienen mediante enumerator.Current.Item1 y enumerator.Current.Item2. El enumerador se consigue con
         * this.GetColumnAndDataEnumerator();
         * 3. Ir recorriendo el enumerador, recogiendo el objeto celda de la row creada, y modificando su data con el valor de enumerator.Current.Item2
         * 4. Se finaliza el bucle, y se inserta en la tabla 
         * 
         * Realmente asi es como estaba, pero con eso de que se me olvidan las cosas y que en clase no me concentro del todo y no me empano de lo que tenia en mente, ahora al hacer
         * esa modificacion, he cambiado lo de el tipo de coleccion de los nombres de las columnas y los datos (perdon por tanto marear) (comente el codigo para que no haya errores de
         * compilacion)
         */

        public override void ExecuteParticularQueryAction(Table table)
        {
            
            /**Row row = table.CreateRowDefinition();
            for (int i = 0; i< selectedColumnNames.Count; i++)
            {
                row.GetCell(selectedColumnNames[i]).data = insertedDate[i];
            }
            table.AddRow(row);*/
            
        }
  
    }
}
