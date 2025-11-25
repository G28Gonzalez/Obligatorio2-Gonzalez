using Dominio;

namespace Obligatorio
{
    internal class Program
    {
        private static Sistema sistema;

        static void Main(string[] args)
        {
            sistema = new Sistema();
            string opcion = "";
            while (opcion != "5")
            {
                mostrarMenu();
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        {
                            Console.Clear();
                            //Listado de todos los usuarios. Se va a mostrar nombre, mail y grupo.
                            Console.WriteLine("Listado de usuarios");
                            Console.WriteLine("");
                            sistema.MostrarListadoDeUsuarios();
                            Console.WriteLine("");
                        }
                        break;

                    case "2":
                        {
                            Console.Clear();
                            Console.WriteLine("Ingrese el email del usuario:");
                            string email = Console.ReadLine();

                            //Validar que el email no sea nulo o vacío

                            if (string.IsNullOrEmpty(email))
                            {
                                Console.WriteLine("El email no puede estar vacío.");
                                return;
                            }
                            //Si todo es correcto on la variable email llamamos al metodo MostrarPagosPorEmail
                            sistema.MostrarPagosPorEmail(email);
                        }
                        break;

                    case "3":
                        {
                            Console.Clear();
                            //Alta de usuario.

                            Console.WriteLine("Nombre");
                            string Nombre = Console.ReadLine();
                            Console.WriteLine("Apellido");
                            string Apellido = Console.ReadLine();

                            //Validar que el nombre y apellido no sean nulos o vacíos

                            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido))
                            {
                                Console.WriteLine("El nombre y/o el apellido no pueden estar vacíos. Intente nuevamente.");
                                return;
                            }

                            //Contraseña

                            Console.WriteLine("Contraseña");
                            string Contrasenia = Console.ReadLine();

                            //Validar que la contraseña tenga al menos 8 caracteres y no sea nula o vacía

                            if (Contrasenia.Length < 8 || string.IsNullOrEmpty(Contrasenia))
                            {
                                Console.WriteLine("La contraseña debe tener al menos 8 caracteres. Intente nuevamente.");
                                return;
                            }

                            //Equipos

                            Console.WriteLine("Equipos");

                            //Recorrer la lista de equipos y mostrar sus nombres con un número para seleccionar
                            for (int i = 0; i < sistema.Equipos.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {sistema.Equipos[i].NombreEquipo}");
                            }
                            Console.WriteLine("Seleccione el numero del equipo");

                            //Seleccionar equipo por número
                            int indice;
                            //Validar que el número nos sirva
                            while (!int.TryParse(Console.ReadLine(), out indice) || indice < 1 || indice > sistema.Equipos.Count)
                            {
                                Console.WriteLine("Número inválido. Intente nuevamente:");
                            }

                            Equipo equipoSeleccionado = sistema.Equipos[indice - 1];

                            //Fecha de Incorporacion
                            Console.WriteLine("Fecha de incorporación (dd/mm/aaaa):");
                            DateTime FechaIncorporacion;
                            while (!DateTime.TryParse(Console.ReadLine(), out FechaIncorporacion))
                            {
                                Console.WriteLine("Formato incorrecto. Ingrese la fecha nuevamente (dd/mm/aaaa):");
                            }
                            //Creamos el usuario
                            sistema.CrearUsuario(Nombre, Apellido, Contrasenia, equipoSeleccionado, FechaIncorporacion);
                        }
                        break;

                    case "4":
                        {
                            Console.Clear();
                            //Mostrar usuarios de un equipo
                            Console.WriteLine("Equipos");

                            //Recorrer la lista de equipos y mostrar sus nombres con un número para seleccionar
                            for (int i = 0; i < sistema.Equipos.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {sistema.Equipos[i].NombreEquipo}");
                            }
                            Console.WriteLine("Seleccione el numero del equipo");
                            int indice;
                            //Validar que el número nos sirva
                            while (!int.TryParse(Console.ReadLine(), out indice) || indice < 1 || indice > sistema.Equipos.Count)
                            {
                                Console.WriteLine("Número inválido. Intente nuevamente:");
                            }
                            //En base al indice elegido mostramos los usuarios del equipo
                            sistema.MostrarUsuariosDeEquipo(indice);
                            Console.WriteLine("");
                        }
                        break;

                    case "5":
                        {
                            Console.Clear();
                            Console.WriteLine("Cerrando programa...");
                        }
                        break;

                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Seleccione una opcion valida");
                            Console.WriteLine("");
                        }
                        break;
                }

                static void mostrarMenu()
                {
                    Console.WriteLine("MENU DE GESTION DE GASTOS");
                    Console.WriteLine(" ");
                    Console.WriteLine("1 - Listar Todos Los Usuarios");
                    Console.WriteLine("2 - Listar Pagos De Un Usuario");
                    Console.WriteLine("3 - Crear Usuario");
                    Console.WriteLine("4 - Mostrar Listado De Un Equipo");
                    Console.WriteLine("5 - Cerrar Programa");
                }
            }
        }
    }
}

