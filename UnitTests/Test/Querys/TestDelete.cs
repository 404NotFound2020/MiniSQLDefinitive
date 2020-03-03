using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Test.Querys
{
    [TestClass]
    public class TestDelete
    {

        //Sorry but i will put this in spanish 
        /**Usad una query del tipo select para seleccionar las rows que se quieren eliminar, una vez seleccionadas ejecutar el delete con la misma where que el select
         * tras eso, comparar que las rows de la select y las de la delete (porque ambas clases almacenan una lista de row que fueron seleccionadas o borradas) son las mismas
         * (usad el comparador), y finalmente, volver a aplicar otra select con la misma where y verificar que las rows afectadas fueron 0
        **/
        [TestMethod]
        public void DeleteRow_GodArguments_RowsDeleted()
        {

        }

        [TestMethod]
        public void DeleteRow_BadArgumentsTableDoesntExist_NotifiedInResult()
        {

        }

        [TestMethod]
        public void DeleteRow_BadArgumentsInWhere_NotifiedInResult()
        {

        }

    }
}
