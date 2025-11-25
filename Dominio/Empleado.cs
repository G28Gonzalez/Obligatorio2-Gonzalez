using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleado : Usuario
    {
        public Empleado(string nombre, string apellido, string contrasenia, Equipo equipo, DateTime fechaIncorporacion) : base(nombre, apellido, contrasenia, equipo, fechaIncorporacion)
        {
        }

        public override string Rol()
        {
            return "Empleado";
        }


    }
}
