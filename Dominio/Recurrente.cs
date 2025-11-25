using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Recurrente : Pago
    {
        #region Propiedades
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool SinLimite { get; private set; } 

        #endregion

        #region Constructores
        public Recurrente(MetodoPago metodoPago, TipoGasto tipoGasto, Usuario usuarioQueRealizo, string descripcion, double monto, DateTime fechaInicio, DateTime? fechaFin = null)
            : base(metodoPago, tipoGasto, usuarioQueRealizo, descripcion, monto)
        {
            if (fechaFin.HasValue)
            {
                FechaInicio = fechaInicio;
                FechaFin = fechaFin.Value;
                SinLimite = false;
            }
            else
            {
                // Si no le doy fecha fin, se interpreta como "sin límite"
                FechaInicio = fechaInicio;
                FechaFin = DateTime.MaxValue;
                SinLimite = true;
            }
        }

     
        #endregion


        #region Metodos
        public override string ToString()
        {
            return  $"Fecha Inicio: {FechaInicio.ToShortDateString()}, Fecha Fin: {FechaFin.ToShortDateString()}";
        }

        public double CalcularMonto() {
            double recargo;
            int meses = ((FechaFin.Year - FechaInicio.Year) * 12) + FechaFin.Month - FechaInicio.Month + 1;

            if (meses <= 5 || SinLimite)
            {
                return Monto * 1.03;
            }

            if (meses > 10)
            {
                recargo = 0.10;
            } else if (meses >= 6)
            {
                recargo = 0.05;
            } else
            {
                recargo = 0.03;
            }

            return (Monto * (1 + recargo)) * meses;

        }
        #endregion
    }
}
