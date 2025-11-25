using System;
using Dominio;

namespace Obligatorio
{
    internal class Program
    {
        private static Sistema sistema;

        static void Main(string[] args)
        {
            sistema = Sistema.Instancia;

            string opcion = "";
            while (opcion != "5")
            {
                MostrarMenu();
                Console.Write("Ingrese una opción: ");
                opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        ListarUsuarios();
                        break;

                    case "2":
                        ListarPagosPorUsuario();
                        break;

                    case "3":
                        AltaUsuario();
                        break;

                    case "4":
                        MostrarUsuariosPorEquipo();
                        break;

                    case "5":
                        Console.WriteLine("Cerrando programa...");
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intente nuevamente.\n");
                        break;
                }

                if (opcion != "5")
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("MENU DE GESTION DE GASTOS");
            Console.WriteLine("-------------------------");
            Console.WriteLine("1 - Listar todos los usuarios");
            Console.WriteLine("2 - Listar pagos de un usuario");
            Console.WriteLine("3 - Crear usuario");
            Console.WriteLine("4 - Mostrar listado de un equipo");
            Console.WriteLine("5 - Cerrar programa");
            Console.WriteLine();
        }

        private static void ListarUsuarios()
        {
            Console.WriteLine("Listado de usuarios\n");

            if (sistema.Usuarios.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
                return;
            }

            foreach (Usuario usuario in sistema.Usuarios)
            {
                Console.WriteLine($"Nombre: {usuario.Nombre} {usuario.Apellido} | Email: {usuario.Email} | Equipo: {usuario.Equipo.NombreEquipo}");
            }

            Console.WriteLine();
        }

        private static void ListarPagosPorUsuario()
        {
            Console.Write("Ingrese el email del usuario: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("El email no puede estar vacío.");
                return;
            }

            Console.WriteLine($"\nPagos realizados por: {email}");
            Console.WriteLine("-----------------------------------------------------");

            bool encontro = false;

            foreach (Pago p in sistema.Pagos)
            {
                if (p.UsuarioQueRealizo != null &&
                    p.UsuarioQueRealizo.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    encontro = true;

                    string tipo = p is Recurrente ? "Recurrente" : "Único";

                    Console.WriteLine($"ID Pago: {p.Id}");
                    Console.WriteLine($"Descripción: {p.Descripcion}");
                    Console.WriteLine($"Método de pago: {p.MetodoDePago}");
                    Console.WriteLine($"Tipo: {tipo}");

                    if (p is Unico u)
                    {
                        double total = u.CalcularMonto();
                        Console.WriteLine($"Monto total: ${total:0.##}");
                    }
                    else if (p is Recurrente r)
                    {
                        double total = r.CalcularMonto();
                        Console.WriteLine($"Monto total: ${total:0.##}");

                        if (r.FechaFin == DateTime.MaxValue)
                        {
                            Console.WriteLine("Recurrente sin fecha de fin");
                        }
                        else if (r.FechaFin < DateTime.Today)
                        {
                            Console.WriteLine("Finalizado");
                        }
                        else
                        {
                            Console.WriteLine($"Fecha fin: {r.FechaFin.ToShortDateString()}");
                        }
                    }

                    Console.WriteLine("-----------------------------------------------------");
                }
            }

            if (!encontro)
            {
                Console.WriteLine("No se encontraron pagos para ese usuario.");
            }
        }

        private static void AltaUsuario()
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            {
                Console.WriteLine("El nombre y/o el apellido no pueden estar vacíos.");
                return;
            }

            Console.Write("Contraseña: ");
            string contrasenia = Console.ReadLine();

            if (string.IsNullOrEmpty(contrasenia) || contrasenia.Length < 8)
            {
                Console.WriteLine("La contraseña debe tener al menos 8 caracteres.");
                return;
            }

            Console.WriteLine("\nEquipos:");
            for (int i = 0; i < sistema.Equipos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sistema.Equipos[i].NombreEquipo}");
            }

            Console.Write("Seleccione el número del equipo: ");
            int indiceEquipo;
            while (!int.TryParse(Console.ReadLine(), out indiceEquipo) ||
                   indiceEquipo < 1 || indiceEquipo > sistema.Equipos.Count)
            {
                Console.Write("Número inválido. Intente nuevamente: ");
            }

            Equipo equipoSeleccionado = sistema.Equipos[indiceEquipo - 1];

            Console.Write("Fecha de incorporación (dd/mm/aaaa): ");
            DateTime fechaIncorporacion;
            while (!DateTime.TryParse(Console.ReadLine(), out fechaIncorporacion))
            {
                Console.Write("Formato incorrecto. Ingrese la fecha nuevamente (dd/mm/aaaa): ");
            }

            sistema.CrearUsuario(nombre, apellido, contrasenia, equipoSeleccionado, fechaIncorporacion);
            Console.WriteLine("\nUsuario creado correctamente.");
        }

        private static void MostrarUsuariosPorEquipo()
        {
            Console.WriteLine("Equipos:");
            for (int i = 0; i < sistema.Equipos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sistema.Equipos[i].NombreEquipo}");
            }

            Console.Write("Seleccione el número del equipo: ");
            int indice;
            while (!int.TryParse(Console.ReadLine(), out indice) ||
                   indice < 1 || indice > sistema.Equipos.Count)
            {
                Console.Write("Número inválido. Intente nuevamente: ");
            }

            Equipo equipoSeleccionado = sistema.Equipos[indice - 1];

            Console.WriteLine($"\nUsuarios del equipo {equipoSeleccionado.NombreEquipo}:\n");

            bool hayUsuarios = false;

            foreach (Usuario u in sistema.Usuarios)
            {
                if (u.Equipo == equipoSeleccionado)
                {
                    hayUsuarios = true;
                    Console.WriteLine($"Nombre: {u.Nombre} {u.Apellido} | Email: {u.Email}");
                }
            }

            if (!hayUsuarios)
            {
                Console.WriteLine("No hay usuarios en ese equipo.");
            }

            Console.WriteLine();
        }
    }
}
