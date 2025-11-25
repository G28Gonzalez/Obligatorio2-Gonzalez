using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Usuario
    {
        //Sistema miSistema = Sistema.Instancia;

        #region Propiedades
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }
        public Equipo Equipo { get; set; }
        public DateTime FechaIncorporacion { get; set; }
        #endregion

        #region Constructores
        public Usuario(string nombre, string apellido, string contrasenia, Equipo equipo, DateTime fechaIncorporacion)
        {
            Nombre = nombre;
            Apellido = apellido;
            Contrasenia = contrasenia;
            Equipo = equipo;
            FechaIncorporacion = fechaIncorporacion;
        }
        #endregion

        #region Metodos

        public abstract string Rol();


        public override string ToString()
        {
            return $"Nombre: {Nombre}, Apellido: {Apellido}, Contraseña: {Contrasenia}, Email {Email}, Equipo: {Equipo.NombreEquipo}, Fecha Incorporacion: {FechaIncorporacion.ToShortDateString()}";
        }

        public override bool Equals(object obj)
        {
            return obj is Usuario usuario &&
                   Email == usuario.Email;
        }

        
        #endregion
    }
}
