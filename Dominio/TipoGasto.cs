using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TipoGasto
    {
        #region Propiedades
        public string Nombre {  get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Constructores
        public TipoGasto (string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }
        #endregion

        #region Metodos
        public override string ToString()
        {
            return $"Nombre: {Nombre}, Descripcion {Descripcion}";
        }
        #endregion
    }
}
