using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pago
    {
        public enum MetodoPago
        {
            Credito, Debito, Efectivo
        }
        #region Propiedades
        public int Id {  get; set; }
        public static int s_ultId { get; set; } = 1;
        public MetodoPago MetodoDePago { get; set; }
        public TipoGasto TipoGasto { get; set; }
        public Usuario UsuarioQueRealizo { get; set; }
        public string Descripcion {  get; set; }
        public double Monto { get; set; }
        #endregion

        #region Constructores
        public Pago (MetodoPago metodoPago, TipoGasto tipoGasto, Usuario usuarioQueRealizo, string descripcion, double monto)
        {
            Id = s_ultId++;
            MetodoDePago = metodoPago;
            TipoGasto = tipoGasto;
            UsuarioQueRealizo = usuarioQueRealizo;
            Descripcion = descripcion;
            Monto = monto;
        }
        #endregion

        #region Metodos
        public override string ToString()
        {
            return $"Id: {Id}, Metodo Pago: {MetodoDePago}, Tipo Pago: {TipoGasto}, Usuario: {UsuarioQueRealizo.Nombre}, Descripcion: {Descripcion}";
        }
        #endregion
    }
}
