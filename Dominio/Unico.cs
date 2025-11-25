using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Unico : Pago
    {
        #region Propiedades
        public DateTime FechaPago { get; set; }
        public int NroRecibo { get; set; }
        #endregion

        #region Constructores
        public Unico(MetodoPago metodoPago, TipoGasto tipoGasto, Usuario usuarioQueRealizo, string descripcion, double monto, DateTime fechaPago, int nroRecibo)
            : base(metodoPago, tipoGasto, usuarioQueRealizo, descripcion, monto)
        {
            FechaPago = fechaPago;
            NroRecibo = nroRecibo;
        }
        #endregion

        #region Metodos
        public override string ToString()
        {
            return $"Fecha Pago: {FechaPago.ToShortDateString()}, Nro Recibo: {NroRecibo}";
        }

        public double CalcularMonto()
        {
            double descuento;
            if (MetodoDePago == Pago.MetodoPago.Efectivo)
            {
                descuento = 0.8;
            }
            else
            {
                descuento = 0.9;
            }
                return Monto * descuento;
        }
        #endregion
    }
}
