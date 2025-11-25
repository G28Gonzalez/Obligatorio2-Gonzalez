using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Dominio.Pago;

namespace Dominio
{
    public class Sistema
    {
        private static Sistema _instancia;
        public static Sistema Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new Sistema();
                return _instancia;
            }
        }

        public Sistema()
        {
            PrecargaDatos();
        }

        //Listas Privadas
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Equipo> _equipos = new List<Equipo>();
        private List<TipoGasto> _gastos = new List<TipoGasto>();
        private List<Pago> _pagos = new List<Pago>();

        //Lista de propiedades para entrar a listas privadas
        public List<Usuario> Usuarios
        {
            get { return _usuarios; }
        }
        public List<Equipo> Equipos
        {
            get { return _equipos; }
        }
        public List<TipoGasto> Gastos
        {
            get { return _gastos; }
        }
        public List<Pago> Pagos
        {
            get { return _pagos; }
        }

        #region PrecargaDatos
        public void PrecargaDatos()
        {
            // =======================
            // Equipos
            // =======================
            var desarrollo = new Equipo("Desarrollo"); AgregarEquipo(desarrollo);
            var qa = new Equipo("QA"); AgregarEquipo(qa);
            var soporte = new Equipo("Soporte"); AgregarEquipo(soporte);
            var administracion = new Equipo("Administración"); AgregarEquipo(administracion);

            // =======================
            // Usuarios (con roles)
            // =======================

            // ===== Desarrollo (6) =====
            var gastonPerez = CrearUsuario("Gaston", "Perez", "gaston1234", desarrollo, new DateTime(2020, 1, 15), "Gerente");
            var luciaMendez = CrearUsuario("Lucia", "Mendez", "lucia4567", desarrollo, new DateTime(2021, 5, 22), "Empleado");
            var martinLopez = CrearUsuario("Martin", "Lopez", "martin7890", desarrollo, new DateTime(2019, 11, 3), "Empleado");
            var valentinaSuarez = CrearUsuario("Valentina", "Suarez", "valen3219", desarrollo, new DateTime(2022, 2, 10), "Empleado");
            var juanDominguez = CrearUsuario("Juan", "Dominguez", "juan6548", desarrollo, new DateTime(2023, 6, 1), "Empleado");
            var nicolasRamos = CrearUsuario("Nicolas", "Ramos", "nico9876", desarrollo, new DateTime(2018, 8, 27), "Empleado");

            // ===== QA (5) =====
            var sofiaFernandez = CrearUsuario("Sofia", "Fernandez", "sofia1597", qa, new DateTime(2021, 3, 5), "Gerente");
            var federicoTorres = CrearUsuario("Federico", "Torres", "fede7531", qa, new DateTime(2020, 12, 14), "Empleado");
            var camilaGomez = CrearUsuario("Camila", "Gomez", "cami2589", qa, new DateTime(2022, 7, 19), "Empleado");
            var rodrigoAcosta = CrearUsuario("Rodrigo", "Acosta", "rodo9513", qa, new DateTime(2023, 9, 9), "Empleado");
            var emiliaSantos = CrearUsuario("Emilia", "Santos", "emilia8520", qa, new DateTime(2019, 4, 18), "Empleado");

            // ===== Soporte (6) =====
            var diegoSilva = CrearUsuario("Diego", "Silva", "diego1475", soporte, new DateTime(2018, 12, 7), "Gerente");
            var fernandaRey = CrearUsuario("Fernanda", "Rey", "fer96320", soporte, new DateTime(2020, 10, 29), "Empleado");
            var andresMoran = CrearUsuario("Andres", "Moran", "andres753", soporte, new DateTime(2021, 8, 11), "Empleado");
            var paulaRodriguez = CrearUsuario("Paula", "Rodriguez", "paula9510", soporte, new DateTime(2022, 5, 2), "Empleado");
            var tomasCastro = CrearUsuario("Tomas", "Castro", "tomi3571", soporte, new DateTime(2023, 1, 23), "Empleado");
            var brunoAlvarez = CrearUsuario("Bruno", "Alvarez", "bruno2580", soporte, new DateTime(2019, 9, 14), "Empleado");

            // ===== Administración (5) =====
            var mariaPereira = CrearUsuario("Maria", "Pereira", "maria6547", administracion, new DateTime(2020, 2, 8), "Gerente");
            var julietaRossi = CrearUsuario("Julieta", "Rossi", "julieta159", administracion, new DateTime(2021, 6, 30), "Empleado");
            var leandroCosta = CrearUsuario("Leandro", "Costa", "leandro357", administracion, new DateTime(2019, 10, 17), "Empleado");
            var veronicaDiaz = CrearUsuario("Veronica", "Diaz", "vero75320", administracion, new DateTime(2022, 11, 25), "Empleado");
            var carlosVega = CrearUsuario("Carlos", "Vega", "carlos8521", administracion, new DateTime(2023, 4, 4), "Empleado");

            // =======================
            // Tipos de Gasto (10)
            // =======================
            var luz = new TipoGasto("Luz", "Gasto de luz mensual"); AgregarTipoGasto(luz);
            var agua = new TipoGasto("Agua", "Servicio de agua"); AgregarTipoGasto(agua);
            var internet = new TipoGasto("Internet", "Servicio de internet"); AgregarTipoGasto(internet);
            var telefono = new TipoGasto("Telefonía", "Línea fija/celular"); AgregarTipoGasto(telefono);
            var alquiler = new TipoGasto("Alquiler", "Renta oficina"); AgregarTipoGasto(alquiler);
            var netflix = new TipoGasto("Netflix", "Suscripción Netflix"); AgregarTipoGasto(netflix);
            var spotify = new TipoGasto("Spotify", "Suscripción Spotify"); AgregarTipoGasto(spotify);
            var seguros = new TipoGasto("Seguros", "Seguro multi-riesgo"); AgregarTipoGasto(seguros);
            var mantenimiento = new TipoGasto("Mantenimiento", "Servicios técnicos"); AgregarTipoGasto(mantenimiento);
            var equipamiento = new TipoGasto("Equipamiento", "Compra de equipamiento"); AgregarTipoGasto(equipamiento);

            // =======================
            // Pagos
            // =======================

            // 17 PAGOS ÚNICOS
            AgregarPago(new Unico(Pago.MetodoPago.Credito, luz, gastonPerez, "Pago de luz oficina", 3000, new DateTime(2025, 10, 9), 1001));
            AgregarPago(new Unico(Pago.MetodoPago.Debito, agua, luciaMendez, "Pago de agua setiembre", 2100, new DateTime(2025, 9, 30), 1002));
            AgregarPago(new Unico(Pago.MetodoPago.Efectivo, internet, martinLopez, "Pago puntual internet", 1920, new DateTime(2025, 8, 15), 1003));
            AgregarPago(new Unico(Pago.MetodoPago.Credito, telefono, valentinaSuarez, "Recarga telefonía móvil", 650, new DateTime(2025, 7, 12), 1004));
            AgregarPago(new Unico(Pago.MetodoPago.Debito, mantenimiento, juanDominguez, "Mantenimiento impresoras", 4800, new DateTime(2025, 6, 10), 1005));
            AgregarPago(new Unico(Pago.MetodoPago.Credito, equipamiento, nicolasRamos, "Compra mouse ergonómicos", 7200, new DateTime(2025, 5, 28), 1006));
            AgregarPago(new Unico(Pago.MetodoPago.Efectivo, seguros, sofiaFernandez, "Pago seguro anual", 15500, new DateTime(2025, 3, 20), 1007));
            AgregarPago(new Unico(Pago.MetodoPago.Credito, alquiler, federicoTorres, "Ajuste de garantía", 11000, new DateTime(2025, 2, 5), 1008));
            AgregarPago(new Unico(Pago.MetodoPago.Debito, mantenimiento, camilaGomez, "Cambio filtros A/C", 3600, new DateTime(2025, 1, 18), 1009));
            AgregarPago(new Unico(Pago.MetodoPago.Efectivo, equipamiento, rodrigoAcosta, "Teclados mecánicos", 9800, new DateTime(2024, 12, 2), 1010));
            AgregarPago(new Unico(Pago.MetodoPago.Credito, telefono, emiliaSantos, "Reposición chips SIM", 900, new DateTime(2024, 11, 21), 1011));
            AgregarPago(new Unico(Pago.MetodoPago.Debito, agua, diegoSilva, "Pago de agua extraordinario", 2500, new DateTime(2024, 10, 25), 1012));
            AgregarPago(new Unico(Pago.MetodoPago.Credito, luz, fernandaRey, "Pago de luz extraordinario", 3400, new DateTime(2024, 9, 30), 1013));
            AgregarPago(new Unico(Pago.MetodoPago.Efectivo, equipamiento, andresMoran, "Sillas ergonómicas (pago contado)", 19900, new DateTime(2024, 8, 10), 1014));
            AgregarPago(new Unico(Pago.MetodoPago.Debito, mantenimiento, paulaRodriguez, "Desinfección oficina", 2800, new DateTime(2024, 7, 5), 1015));
            AgregarPago(new Unico(Pago.MetodoPago.Credito, telefono, tomasCastro, "Teléfono IP recepción", 5200, new DateTime(2024, 6, 12), 1016));
            AgregarPago(new Unico(Pago.MetodoPago.Debito, seguros, brunoAlvarez, "Endoso equipo portátil", 3100, new DateTime(2024, 5, 8), 1017));

            // 25 PAGOS RECURRENTES

            // FINALIZADOS
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, internet, mariaPereira, "Internet backup secundario", 1400, new DateTime(2023, 10, 1), new DateTime(2025, 5, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, spotify, veronicaDiaz, "Spotify ambientación", 450, new DateTime(2024, 2, 1), new DateTime(2025, 1, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, netflix, leandroCosta, "Netflix oficina", 550, new DateTime(2024, 1, 1), new DateTime(2024, 12, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Efectivo, telefono, carlosVega, "Paquete telefonía equipo ventas", 1200, new DateTime(2024, 3, 1), new DateTime(2025, 3, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, mantenimiento, luciaMendez, "Mantenimiento servidores (mensual)", 3800, new DateTime(2024, 4, 1), new DateTime(2025, 4, 1)));

            // VIGENTES
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, alquiler, gastonPerez, "Alquiler oficina centro", 45000, new DateTime(2024, 1, 1), new DateTime(2026, 12, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, luz, martinLopez, "Luz mensual sede principal", 2700, new DateTime(2024, 1, 1), new DateTime(2026, 1, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, agua, valentinaSuarez, "Agua mensual sede principal", 1900, new DateTime(2024, 1, 1), new DateTime(2026, 1, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, internet, juanDominguez, "Internet fibra 500 Mbps", 3200, new DateTime(2024, 1, 1), new DateTime(2027, 1, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, telefono, nicolasRamos, "Telefonía corporativa", 1500, new DateTime(2024, 1, 1), new DateTime(2026, 6, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, seguros, camilaGomez, "Seguro integral empresa", 12500, new DateTime(2024, 6, 1), new DateTime(2027, 6, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, mantenimiento, rodrigoAcosta, "Soporte PCs mensual", 2600, new DateTime(2024, 5, 1), new DateTime(2026, 5, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, equipamiento, emiliaSantos, "Leasing fotocopiadora", 7800, new DateTime(2024, 9, 1), new DateTime(2026, 9, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, alquiler, diegoSilva, "Cocheras staff", 6000, new DateTime(2025, 2, 1), new DateTime(2026, 8, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, luz, fernandaRey, "Luz anexo", 2100, new DateTime(2025, 3, 1), new DateTime(2026, 3, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, agua, andresMoran, "Agua anexo", 1500, new DateTime(2025, 3, 1), new DateTime(2026, 3, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, internet, paulaRodriguez, "Internet backup LTE", 1600, new DateTime(2025, 4, 1), new DateTime(2026, 4, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, telefono, tomasCastro, "Telefonía call center", 2900, new DateTime(2025, 4, 1), new DateTime(2026, 10, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, netflix, brunoAlvarez, "Netflix sala capacitación", 550, new DateTime(2025, 5, 1), new DateTime(2026, 5, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, spotify, mariaPereira, "Spotify open space", 450, new DateTime(2025, 5, 1), new DateTime(2026, 5, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, seguros, julietaRossi, "Seguro electrónica", 4300, new DateTime(2025, 6, 1), new DateTime(2027, 6, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, mantenimiento, leandroCosta, "Mantenimiento aire acondicionado", 3100, new DateTime(2025, 6, 1), new DateTime(2026, 12, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, equipamiento, veronicaDiaz, "Arrendamiento notebooks", 11200, new DateTime(2025, 7, 1), new DateTime(2027, 7, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, alquiler, carlosVega, "Depósito archivo", 7200, new DateTime(2025, 7, 1), new DateTime(2026, 7, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, luz, luciaMendez, "Luz depósito", 1850, new DateTime(2025, 8, 1), new DateTime(2026, 8, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, agua, martinLopez, "Agua depósito", 1200, new DateTime(2025, 8, 1), new DateTime(2026, 8, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, internet, valentinaSuarez, "Internet sala de reuniones", 2100, new DateTime(2025, 9, 1), new DateTime(2026, 9, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, telefono, juanDominguez, "Telefonía móviles internos", 1700, new DateTime(2025, 9, 1), new DateTime(2026, 9, 1)));

            // SIN FECHA FIN
            AgregarPago(new Recurrente(Pago.MetodoPago.Credito, netflix, sofiaFernandez, "Netflix sala recreativa", 550, new DateTime(2025, 1, 1)));
            AgregarPago(new Recurrente(Pago.MetodoPago.Debito, spotify, federicoTorres, "Spotify recepción", 450, new DateTime(2025, 1, 1)));
        }
        #endregion

        #region Métodos para agregar a las listas privadas

        public void AgregarUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }
        public void AgregarEquipo(Equipo equipo)
        {
            _equipos.Add(equipo);
        }
        public void AgregarTipoGasto(TipoGasto tipoGasto)
        {
            _gastos.Add(tipoGasto);
        }

        // Ver si un tipo de gasto está siendo usado por algún pago
        public bool TipoGastoEnUso(TipoGasto tipo)
        {
            foreach (Pago p in _pagos)
            {
                // AJUSTÁ ESTA LÍNEA SI TU PROPIEDAD SE LLAMA DISTINTO
                if (p.TipoGasto == tipo)
                {
                    return true;
                }
            }
            return false;
        }

        // Eliminar tipo de gasto de la lista (ya validado antes)
        public void EliminarTipoGasto(TipoGasto tipo)
        {
            _gastos.Remove(tipo);
        }

        public void AgregarPago(Pago pago)
        {
            _pagos.Add(pago);
        }
        #endregion

        #region Crear Usuario
        public Usuario CrearUsuario(string unNombre, string unApellido, string unaContrasenia,
                            Equipo unEquipoSeleccionado, DateTime unaFechaIncorporacion)
        {
            // Redirige a la sobrecarga con rol "Empleado"
            return CrearUsuario(unNombre, unApellido, unaContrasenia,
                                unEquipoSeleccionado, unaFechaIncorporacion, "Empleado");
        }

        public Usuario CrearUsuario(string unNombre, string unApellido, string unaContrasenia, Equipo unEquipoSeleccionado, DateTime unaFechaIncorporacion, string Rol)
        {
            Usuario usuario = CrearUsuarioPorRol(Rol, unNombre, unApellido, unaContrasenia, unEquipoSeleccionado, unaFechaIncorporacion);

            //Llamamos al método para generar nuestros emails
            GenerarEmailValido(usuario);

            try
            {
                AgregarUsuario(usuario);
                Console.WriteLine($"Nombre: {usuario.Nombre} - Apellido: {usuario.Apellido} - Rol: {usuario.Rol} - Contraseña: {usuario.Contrasenia} - Email: {usuario.Email} - Equipo: {usuario.Equipo} - Fecha Incorporacion {unaFechaIncorporacion.ToShortDateString()}");
                Console.WriteLine("Usuario creado con exito");
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }

            return usuario;
        }

        private Usuario CrearUsuarioPorRol(string rol, string nombre, string apellido, string pass,
                                   Equipo equipo, DateTime fechaIng)
        {
            if (string.IsNullOrWhiteSpace(rol))
                throw new ArgumentException("Rol vacío o nulo. Use 'Gerente' o 'Empleado'.");

            string canon = rol.Trim().ToLowerInvariant();

            switch (canon)
            {
                case "gerente":
                    return new Gerente(nombre, apellido, pass, equipo, fechaIng);

                case "empleado":
                    return new Empleado(nombre, apellido, pass, equipo, fechaIng);

                default:
                    throw new ArgumentException($"Rol no válido: '{rol}'. Use 'Gerente' o 'Empleado'.");
            }
        }
        #endregion

        #region Listar Usuarios
        public void MostrarListadoDeUsuarios()
        {
            if (Usuarios.Count == 0)
            {
                Console.WriteLine("No hay usuarios para mostrar.");
                return;
            }
            else
            {
                foreach (Usuario usuario in Usuarios)
                {
                    Console.WriteLine($"Nombre: {usuario.Nombre} | Email: {usuario.Email} | Equipo: {usuario.Equipo}");
                }
            }
        }
        #endregion

        #region Generar Emails
        public void GenerarEmailValido(Usuario unUsuario)
        {
            string parteNombre = "";
            string parteApellido = "";
            string mail = "";
            string dominio = "@laEmpresa.com";

            if (unUsuario.Nombre.Length <= 3 && unUsuario.Apellido.Length <= 3)
            {
                parteNombre = unUsuario.Nombre;
                parteApellido = unUsuario.Apellido;
                mail = parteNombre + parteApellido + dominio;
            }
            else if (unUsuario.Nombre.Length <= 3 && unUsuario.Apellido.Length > 3)
            {
                parteNombre = unUsuario.Nombre;
                parteApellido = unUsuario.Apellido.Substring(0, 3);
                mail = parteNombre + parteApellido + dominio;
            }
            else if (unUsuario.Nombre.Length > 3 && unUsuario.Apellido.Length <= 3)
            {
                parteNombre = unUsuario.Nombre.Substring(0, 3);
                parteApellido = unUsuario.Apellido;
                mail = parteNombre + parteApellido + dominio;
            }
            else
            {
                parteNombre = unUsuario.Nombre.Substring(0, 3);
                parteApellido = unUsuario.Apellido.Substring(0, 3);
                mail = parteNombre + parteApellido + dominio;
            }
            unUsuario.Email = mail.ToLower();

            //Verificar que no sea duplicado, si es duplicado agrega un numero al final
            int contador = 1;
            while (_usuarios.Contains(unUsuario))
            {
                mail = parteNombre + parteApellido + contador + dominio;
                contador++;
                unUsuario.Email = mail.ToLower();
            }
        }
        #endregion

        #region Mostrar por equipo
        public void MostrarUsuariosDeEquipo(int unIndice)
        {
            Equipo equipoSeleccionado = Equipos[unIndice - 1];

            foreach (Usuario usuario in Usuarios)
            {
                if (usuario.Equipo == equipoSeleccionado)
                {
                    Console.WriteLine($"Nombre: {usuario.Nombre} | Email: {usuario.Email}");
                }
            }
        }
        #endregion

        #region Mostrar Pagos por Email
        public void MostrarPagosPorEmail(string email)
        {
            List<Pago> resultados = new List<Pago>();

            foreach (Pago p in _pagos)
            {
                if (p.UsuarioQueRealizo != null &&
                    p.UsuarioQueRealizo.Email.ToLower() == email.ToLower())
                {
                    resultados.Add(p);
                }
            }

            int MesesInclusive(DateTime desde, DateTime hasta)
            {
                if (hasta < desde) return 0;
                return (hasta.Year - desde.Year) * 12 + (hasta.Month - desde.Month) + 1;
            }

            Console.Clear();
            Console.WriteLine($"Pagos realizados por: {email}");
            Console.WriteLine("-----------------------------------------------------");

            if (resultados.Count == 0)
            {
                Console.WriteLine("No se encontraron pagos para ese usuario.");
                return;
            }

            foreach (Pago p in resultados)
            {
                string tipo;

                if (p is Recurrente)
                {
                    tipo = "Recurrente";
                }
                else
                {
                    tipo = "Único";
                }

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

                    if (p is Recurrente pagoRecurrente)
                    {
                        if (pagoRecurrente.FechaFin == DateTime.MaxValue)
                        {
                            Console.WriteLine("Recurrente");
                        }
                        else if (pagoRecurrente.FechaFin < DateTime.Today)
                        {
                            Console.WriteLine($"Finalizado");
                        }
                        else
                        {
                            Console.WriteLine($"Fecha fin:    {pagoRecurrente.FechaFin.ToShortDateString()}");

                            DateTime hoy = DateTime.Today;
                            DateTime iniMesActual = new DateTime(hoy.Year, hoy.Month, 1);
                            DateTime iniMesInicio = new DateTime(pagoRecurrente.FechaInicio.Year, pagoRecurrente.FechaInicio.Month, 1);
                            DateTime iniMesFin = new DateTime(pagoRecurrente.FechaFin.Year, pagoRecurrente.FechaFin.Month, 1);

                            int pendientes;
                            if (iniMesActual < iniMesInicio)
                            {
                                pendientes = MesesInclusive(iniMesInicio, iniMesFin);
                            }
                            else if (iniMesActual > iniMesFin)
                            {
                                pendientes = 0;
                            }
                            else
                            {
                                pendientes = MesesInclusive(iniMesActual, iniMesFin);
                            }
                            Console.WriteLine($"Pendientes: {pendientes}");
                        }
                    }
                }
                Console.WriteLine("-----------------------------------------------------");
            }
        }
        #endregion

        #region Pagos por mes
        public List<Pago> ObtenerPagosDelMesPorEmail(string email)
        {
            List<Pago> pagosDelMes = new List<Pago>();

            DateTime hoy = DateTime.Today;
            int mesActual = hoy.Month;
            int anioActual = hoy.Year;

            foreach (Pago pago in _pagos)
            {
                if (pago.UsuarioQueRealizo != null &&
                    pago.UsuarioQueRealizo.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    if (pago is Unico unPago)
                    {
                        if (unPago.FechaPago.Month == mesActual &&
                            unPago.FechaPago.Year == anioActual)
                        {
                            pagosDelMes.Add(pago);
                        }
                    }
                    else if (pago is Recurrente rec)
                    {
                        if (rec.FechaInicio.Month == mesActual &&
                            rec.FechaInicio.Year == anioActual)
                        {
                            pagosDelMes.Add(pago);
                        }
                    }
                }
            }

            // Ordenar por monto descendente sin LINQ ni lambdas (burbuja)
            for (int i = 0; i < pagosDelMes.Count - 1; i++)
            {
                for (int j = 0; j < pagosDelMes.Count - i - 1; j++)
                {
                    if (pagosDelMes[j].Monto < pagosDelMes[j + 1].Monto)
                    {
                        Pago aux = pagosDelMes[j];
                        pagosDelMes[j] = pagosDelMes[j + 1];
                        pagosDelMes[j + 1] = aux;
                    }
                }
            }

            return pagosDelMes;
        }
        #endregion

        #region Login
        public Usuario Login(string email, string contrasenia)
        {
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
                    && usuario.Contrasenia == contrasenia)
                {
                    return usuario;
                }
            }
            return null;
        }
        #endregion

        #region Total Gastado
        public double TotalGastado(string email)
        {
            double total = 0;

            List<Pago> pagosDelMes = ObtenerPagosDelMesPorEmail(email);

            foreach (Pago pago in pagosDelMes)
            {
                total += pago.Monto;
            }

            return total;
        }
        #endregion

        #region ObtenerPagosEquipoPorMes
        public List<Pago> ObtenerPagosEquipoPorMes(Usuario gerente, int mes, int anio)
        {
            List<Pago> resultado = new List<Pago>();

            if (gerente == null) return resultado;

            // Equipo del gerente
            Equipo equipoGerente = gerente.Equipo;

            // Todos los usuarios de ese equipo (incluyendo al gerente) sin LINQ
            List<Usuario> usuariosEquipo = new List<Usuario>();
            foreach (Usuario u in _usuarios)
            {
                if (u.Equipo == equipoGerente)
                {
                    usuariosEquipo.Add(u);
                }
            }

            DateTime inicioMes = new DateTime(anio, mes, 1);
            DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

            foreach (Pago pago in _pagos)
            {
                if (pago.UsuarioQueRealizo == null) continue;

                // ¿Quién hizo el pago pertenece al equipo del gerente?
                bool pertenece = false;
                foreach (Usuario u in usuariosEquipo)
                {
                    if (u == pago.UsuarioQueRealizo)
                    {
                        pertenece = true;
                        break;
                    }
                }
                if (!pertenece) continue;

                // Pago único
                if (pago is Unico uPago)
                {
                    if (uPago.FechaPago.Month == mes && uPago.FechaPago.Year == anio)
                    {
                        resultado.Add(pago);
                    }
                }
                // Pago recurrente
                else if (pago is Recurrente r)
                {
                    DateTime fi = r.FechaInicio;
                    DateTime ff = r.FechaFin;

                    bool activoEnMes =
                        fi <= finMes &&
                        (ff == DateTime.MaxValue || ff >= inicioMes);

                    if (activoEnMes)
                    {
                        resultado.Add(pago);
                    }
                }
            }

            // Ordenar por monto descendente sin LINQ
            for (int i = 0; i < resultado.Count - 1; i++)
            {
                for (int j = 0; j < resultado.Count - i - 1; j++)
                {
                    if (resultado[j].Monto < resultado[j + 1].Monto)
                    {
                        Pago aux = resultado[j];
                        resultado[j] = resultado[j + 1];
                        resultado[j + 1] = aux;
                    }
                }
            }

            return resultado;
        }
        #endregion

        #region Totales
        public double TotalBasePagos(List<Pago> pagos)
        {
            double total = 0;
            foreach (Pago p in pagos)
            {
                total += p.Monto;
            }
            return total;
        }

        public double TotalConBeneficios(List<Pago> pagos)
        {
            double total = 0;

            foreach (Pago pago in pagos)
            {
                if (pago is Unico u)
                {
                    total += u.CalcularMonto();
                }
                else if (pago is Recurrente r)
                {
                    total += r.CalcularMonto();
                }
                else
                {
                    total += pago.Monto;
                }
            }

            return total;
        }
        #endregion
    }
}
